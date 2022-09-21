Shader "Custom/bot_navodka_sh"
{	
	Properties{
	//[PerRendererData] не показує
	_C("Color", Color) = (0, 0, 0, 1)
	//	_C2("ColorAtmosphere", Color) = (0, 0, 0, 1)
		_R("Alpha",Range(0,1))=0.0
}

SubShader
{
	Tags
	{
	"Queue" = "Transparent"
	//"IgnoreProjector" = "True"
	//"RenderType" = "Transparent"
	//"PreviewType" = "Plane"
	//"CanUseSpriteAtlas" = "True"
//"Queue" = "Geometry"
	}

	//Cull Off
			//Lighting Off
			//ZTest[unity_GUIZTestMode]

					ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha

	Pass
	{
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