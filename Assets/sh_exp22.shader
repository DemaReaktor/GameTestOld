Shader "Custom/sh_exp22"
{
		SubShader{
		Pass{
Cull front
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
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			fixed4 frag(v2f i) : SV_TARGET{
				return float4(1,1,1,1);
			}

			ENDCG
		}
	}

		FallBack "Standard"
}
