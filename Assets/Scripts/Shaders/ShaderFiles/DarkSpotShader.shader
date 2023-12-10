Shader "Hidden/DarkSpotShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Size("Size", Range(0.0, 1.0)) = 0.5
		_HorizontalLocation("Horizontal Location", Range(-1.0, 1.0)) = 0.0
        _VerticalLocation("Vertical Location", Range(-1.0, 1.0)) = 0.0
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

            uniform float _Size;
            uniform float _HorizontalLocation;
            uniform float _VerticalLocation;

            fixed4 frag (v2f i) : SV_Target
            {                
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed2 pos = i.uv.xy * 2.0 - 1.0;
                pos.xy += fixed2(-1.0 * _HorizontalLocation, -1.0 * _VerticalLocation);
                pos.x = pos.x * _ScreenParams.x / _ScreenParams.y;
                float d = length(pos) - _Size;
                
                col.rgb *= smoothstep(0.0f, 0.4f, d + 0.5);

                return col;
            }
            ENDCG
        }
    }
}
