Shader "Custom/vyb_new_sh"
{
	Properties
	{
		//[PerRendererData] не показує
		 _R("erh",Range(0,100))=0
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
			float _R;
			fixed4 frag(v2f IN) : SV_Target
			{
				return float4(_C.x,_C.y,_C.z,_R);
			}
		ENDCG
		}
	
	}

}