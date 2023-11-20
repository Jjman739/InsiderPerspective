Shader "Hidden/FishEyeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BarrelPower("Barrel Power", Float) = 1.0
        _ExcludeOuterPixels("Exclude Outer Pixels", Integer) = 1
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
            uniform float _BarrelPower;
            uniform int _ExcludeOuterPixels;

            float2 distort(float2 pos)
            {
                float theta = atan2(pos.y, pos.x);
                float radius = length(pos);
                radius = pow(radius, _BarrelPower);
                pos.x = radius * cos(theta);
                pos.y = radius * sin(theta);

                return 0.5 * (pos + 1.0);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 xy = 2.0 * i.uv - 1.0;
                float d = length(xy);

                if (_ExcludeOuterPixels == 1 && d >= 1.0)
                {
                    return fixed4(0.0f, 0.0f, 0.0f, 1.0f);
                }

                float2 uv = distort(xy);
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
