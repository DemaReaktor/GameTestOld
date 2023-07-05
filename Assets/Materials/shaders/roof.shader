Shader "Custom/steliash"
{
    SubShader
    {
        Tags { 		"Queue" = "Transparent"}
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
       CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

      struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float4 pos:TEXCOORD;
			};

			v2f vert(appdata v) {
				v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
				o.pos=mul(unity_ObjectToWorld, v.vertex);				
				return o;
			}
			float4 _T;
			float _Ti;
			fixed4 frag(v2f i) : SV_TARGET{
				if(_Ti+5>= _Time.y &&(_T.x-i.pos.x)*(_T.x-i.pos.x)+(_T.z-i.pos.z)*(_T.z-i.pos.z)<=(_Time.y-_Ti)*(_Time.y-_Ti)*9)
					return float4(sin(_Time.y * 2 + i.pos.x * 0.1) * 0.5 + 0.5, sin(_Time.y * 5 - i.pos.z * 0.1 + i.pos.x * 0.1) * 0.5 + 0.5, sin(_Time.y * 3 + i.pos.z * 0.1) * 0.5 + 0.5, (sin(_Time.y * 1.5 - i.pos.x * 0.14 - i.pos.z * 0.15) * 0.6 + 0.45)*0.2*(5+_Ti-_Time.y));
				else
				return float4(1,1,1,0);
			}

			ENDCG
			}}
				FallBack "Standard"
}
