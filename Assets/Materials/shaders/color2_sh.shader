Shader "Custom/color2_sh"
{
	Properties{
		  _O("Outline", Range(0,1)) = 0.03
		_I2("Color",Color) = (0,0,0,1)
		_I("ColorOsn",Color) = (0,0,0,1)
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		Pass{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			float _O;

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex * (1 - _O));
				return o;
			}

			float4 _I;

			fixed4 frag(v2f i) : SV_TARGET{
								return _I;
				}

				ENDCG
			}

			Pass{
				Cull front

				CGPROGRAM

					#include "UnityCG.cginc"

					#pragma vertex vert
					#pragma fragment frag

					float _O;
				struct appdata {
					float4 vertex : POSITION;
					float2 uv:TEXCOORD;
				};

				struct v2f {
					float4 position : SV_POSITION;
				};

				v2f vert(appdata v) {
					v2f o;
					o.position = UnityObjectToClipPos(v.vertex);
					return o;
				}

				float4 _I2;
				fixed4 frag(v2f i) : SV_TARGET{
					return _I2;
				}

				ENDCG
			}
	}

		FallBack "Standard"
}
