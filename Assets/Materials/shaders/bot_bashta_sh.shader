﻿Shader "Custom/bot_bashta_sh"
{
	Properties{	_C("Color",Color) = (0,0,0,1) }
		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}
		Pass{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag


			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 _C;

			fixed4 frag(v2f i) : SV_TARGET{
								return _C;
				}

				ENDCG
			}

			
	}

		FallBack "Standard"
}
