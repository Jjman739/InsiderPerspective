Shader "Hidden/TVStaticShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Scale("Scale", Range(0.001, 1.0)) = 1.0
		_BlackPixelChance("Black Pixel Chance", Range(0.0, 1.0)) = 0.5
        _Opacity("Opacity", Range(0.0, 1.0)) = 1.0
		_ColorMode("Color  Mode", Integer) = 0
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
            uniform float _Scale;
            uniform float _BlackPixelChance;
            uniform float _Opacity;
            uniform int _ColorMode;

            // Random Function Found Here: https://thebookofshaders.com/10/
            float random(fixed2 st) {
                return frac(sin(dot(st.xy * 50.0, fixed2(12.9898,78.233))) * 43758.5453123);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float time = (1000 + _Time) * _Scale;
                fixed3 randColor = fixed3(
                    // Red Random
                    step(_BlackPixelChance, random(i.uv.xy * time / _ScreenParams.xy)),
                    // Green Random
                    step(_BlackPixelChance, random(i.uv.xy * time / _ScreenParams.xy * (1.0 + (2.0 * _ColorMode)))),
                    // Blue Random
                    step(_BlackPixelChance, random(i.uv.xy * time / _ScreenParams.xy * (1.0 + (3.0 * _ColorMode))))
                );
                
                //col.rgb = step(_BlackPixelChance, rand) * _Opacity;
                col.rgb = lerp(randColor.rgb, col.rgb, 1.0-_Opacity);

                return col;
            }
            ENDCG
        }
    }
}
