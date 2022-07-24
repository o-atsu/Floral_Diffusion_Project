Shader "Hidden/Raymarching"
{
    Properties
    {
		_BackgroundColor ("Background Color", color) = (0, 0, 0, 0)
		_MainColor ("Main Color", color) = (1, 0, 0, 0)
		_MainTex ("Texture", 2D) = "white" {}
		_FoV ("Field of View", Float) = 2.0
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

			#define MAX_MARCH 64
			#define EPS 0.0001


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


			// Properties
			uniform fixed4 _BackgroundColor;
			uniform fixed4 _MainColor;
			sampler2D _MainTex;
			uniform float _FoV;

			/* Calculate Normal */
			float3 getNormal(float3 pos)
			{
				float3 dx = ddx(pos);
				float3 dy = ddy(pos);
				return normalize(cross(dx, dy));
			}


			/* Signed Distance Functions */
			float sphere(float3 pos, float r)
			{
				return length(pos) - r;
			}

			// calculated function
			float sdf(float3 pos)
			{
				return sphere(pos, 1.0);
			}


			/* Raymarching Functions */
			float3 getCameraRayDir(float3 camPos, float3 camTarget, float2 uv)
			{
				float3 camForward = normalize(camTarget - camPos);
				float3 camRight = normalize(cross(float3(0, 1, 0), camForward));
				float3 camUp = normalize(cross(camForward, camRight));

				float3 vDir = normalize((uv.x - 0.5) * camRight + (uv.y - 0.5) * camUp + camForward * _FoV);
				return vDir;
			}
			
			// If ray hits : length of ray, else : -1.0
			float castRay(float3 rayOrigin, float3 rayDir)
			{
				float t = 0.0;

				for(int i = 0;i < MAX_MARCH;i ++){
					float res = sdf(rayOrigin + rayDir * t);
					if(res < EPS * t) {return t;}
					else {t += res;}
				}

				return -1.0; 
			}

			// define color
			fixed4 color(float3 pos)
			{
				return _MainColor;
			}

			// Vertex Shader : default
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

			// Fragment Shader : Raymarching
            fixed4 frag (v2f i) : SV_Target
            {
				float3 camPos = _WorldSpaceCameraPos;
				float3 camTarget = float3(0, 0, 0);
				float3 rayDir = getCameraRayDir(camPos, camTarget, i.uv);

				float t = castRay(camPos, rayDir);
				fixed4 col = _BackgroundColor;

				if(t > 0.0)
				{
					col = color(camPos + rayDir * t);
				}

				return col;
            }
            ENDCG
        }
    }
}
