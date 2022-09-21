Shader "Custom/sh_exp02"
{
    Properties{
      _O("Outline", Range(0,1)) = 0.03
    }

        SubShader{
            Tags{
            "Queue" = "Transparent"
                    "IgnoreProjector" = "True"
                    "RenderType" = "Transparent"
            }



            Pass{
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

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
                    float4 pos:TEXCOORD;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.position = UnityObjectToClipPos(v.vertex * (1 - _O));
                    o.pos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }

                float _V;
                float4 _P;
                fixed4 frag(v2f i) : SV_TARGET{
            if (_V * _V >= (i.pos.x - _P.x) * (i.pos.x - _P.x) + (i.pos.y - _P.y) * (i.pos.y - _P.y) + (i.pos.z - _P.z) * (i.pos.z - _P.z))
                    return fixed4(1, 1, 1, 0);
                    else
                    return fixed4(100, 100, 100, 1);
                }

                ENDCG
            }

           Pass{
                Cull front
                            Blend SrcAlpha OneMinusSrcAlpha

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
                                    float4 pos:TEXCOORD;

                };

                v2f vert(appdata v) {
                    v2f o;
                    o.position = UnityObjectToClipPos(v.vertex);
                                    o.pos = mul(unity_ObjectToWorld, v.vertex);

                    return o;
                }

                float _V;
                float4 _P;

                fixed4 frag(v2f i) : SV_TARGET{
                if (_V * _V >= (i.pos.x - _P.x) * (i.pos.x - _P.x) + (i.pos.y - _P.y) * (i.pos.y - _P.y) + (i.pos.z - _P.z) * (i.pos.z - _P.z))
                    return fixed4(0,0,0,0.1);
                    else
                                    return fixed4(0,0,0,0.99);
                }

                ENDCG
            }
    }

        FallBack "Standard"
}
