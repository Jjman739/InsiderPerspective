Shader "Hidden/ColorInvertShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Invert("Invert", Integer) = 0
        _BlackAndWhite("BlackAndWhite", Integer) = 0
        _Red("Red", Float) = 1.0
        _Green("Green", Float) = 1.0
        _Blue("Blue", Float) = 1.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            uniform int _Invert;
            uniform int _BlackAndWhite;
            uniform float _Red;
            uniform float _Green;
            uniform float _Blue;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                col.rgb *= float3(_Red, _Green, _Blue);

                // Black and White
                if (_BlackAndWhite == 1) {
                    float gray = 0.33 * (col.r + col.g + col.b);
                    col = fixed4(gray, gray, gray, 1.0f);
                }
                // Invert the colors
                if (_Invert == 1) {
                    col.rgb = 1 - col.rgb;
                }
                return col;
            }
            ENDCG
        }
    }
}
