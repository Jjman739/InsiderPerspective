Shader "Hidden/FlipShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_FlipHorizontally("Flip Horizontally", Integer) = 0
		_FlipVertically("Flip Vertically", Integer) = 0
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

            uniform int _FlipHorizontally;
            uniform int _FlipVertically;

            fixed4 frag (v2f i) : SV_Target
            {
                if (_FlipHorizontally == 1) {
                    i.uv.x = 1 - i.uv.x;
                }
                if (_FlipVertically == 1) {
                    i.uv.y = 1 - i.uv.y;
                }
                
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
