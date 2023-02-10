Shader "Custom/strilba_effect_sh"
{
	Properties
	{
		//[PerRendererData] не показує
		_C("Color",Color) = (0,0,0,1)
		_C2("ColorAtmosphere",Color) = (0,0,0,1)
		_R1("osnovaR",Range(0,1000))=0
		_R2("dopR",Range(0,1000))=0
	}

		SubShader
		{
					   Tags{
		"Queue" = "Transparent"
		}
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
		ZWrite off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}
				float4 _C;
				float _R1;

				fixed4 frag(v2f IN) : SV_Target
				{
					return float4(_C.x,_C.y,_C.z,_R1);
				}
			ENDCG
			}
		Pass
			{
					Cull front
		//Lighting off
		ZWrite off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				float _R;
				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex+float4(sin(_Time.y), cos(_Time.y), sin(_Time.y),0)*0.015);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}
				float4 _C2;
				//sampler2D _MainTex;
				float _R2;
				fixed4 frag(v2f IN) : SV_Target
				{
					//float4 v = tex2D(_MainTex, IN.texcoord);
					return float4(_C2.x,_C2.y,_C2.z,_R2);
				}
			ENDCG
			}
Pass
			{
					Cull front
		//Lighting off
		ZWrite off
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				float _R;
				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex + float4(sin(_Time.y+3.14), cos(_Time.y + 3.14), 0,0)*0.015);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}
				float4 _C2;
				//sampler2D _MainTex;
				float _R2;
				fixed4 frag(v2f IN) : SV_Target
				{
					//float4 v = tex2D(_MainTex, IN.texcoord);
					return float4(_C2.x,_C2.y,_C2.z,_R2);
				}
			ENDCG
			}
		}

}