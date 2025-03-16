Shader"Custom/ScrollingTransparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Main texture
        _ScrollSpeed ("Scroll Speed", Vector) = (0.5, 0.5, 0, 0) // Scrolling speed (X, Y)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True"}
        Blend SrcAlpha OneMinusSrcAlpha // Proper transparency blending
        ZWrite Off // Disable depth writing for transparency
        CullOff // Render both sides (important for sprites)

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex; // Texture sampler
float4 _MainTex_ST; // Tiling & Offset
float2 _ScrollSpeed; // Scrolling speed

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply scrolling effect
    float2 offset = _ScrollSpeed * _Time.y;
    o.uv = v.uv * _MainTex_ST.xy + offset;

    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);
                
                // Ensure transparency works correctly
    if (col.a < 0.01)
        discard
