Shader "Hidden/ColorStepper"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Steps("Steps", Integer) = 0
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

            uniform int _Steps;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float stepIntervalHalved = 0.5f / _Steps;
                float stepInterval = stepIntervalHalved * 2.0f;

                if (col.r % stepInterval < stepIntervalHalved) {
                    col.r -= col.r % stepInterval;
                } else {
                    col.r += stepInterval - col.r % stepInterval;
                }

                if (col.g % stepInterval < stepIntervalHalved) {
                    col.g -= col.g % stepInterval;
                } else {
                    col.g += stepInterval - col.g % stepInterval;
                }

                if (col.b % stepInterval < stepIntervalHalved) {
                    col.b -= col.b % stepInterval;
                } else {
                    col.b += stepInterval - col.b % stepInterval;
                }

                return col;
            }
            ENDCG
        }
    }
}
