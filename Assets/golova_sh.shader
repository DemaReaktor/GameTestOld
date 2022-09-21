Shader "Custom/golova_sh"
{
	Properties
	{    _C("Color",Color)=(0,0,0,0)
	}

		SubShader
	{
				   Tags{
		"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
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
				//float4 vertex1   : TEXCOORD1;
				half2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				//OUT.vertex1 = UnityObjectToClipPos(IN.vertex*0.5);
				OUT.texcoord =  mul(unity_ObjectToWorld, IN.vertex);
				return OUT;
			}
			float4 _C;
			float4 _R;
			fixed4 frag(v2f IN) : SV_Target
			{
			//	float r = (IN.texcoord.y-_R.y);
			return  fixed4(_C.x, _C.y, _C.z,1);// IN.vertex);
			}
		ENDCG
		}
	
	}

}