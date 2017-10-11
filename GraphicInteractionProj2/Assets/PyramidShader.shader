Shader "Unlit/PyramidShader"
{
	Properties
	{
		_BumpMap ("Normal Map", 2D) = "bump" {}
      	_Color ("Diffuse Material Color", Color) = (1,1,1,1) 
      	_SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      	_Shininess ("Shininess", Float) = 10
	}

	SubShader
	{
		Tags { "LightMode" = "ForwardBase" } // pass for ambient light and first light source

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

      		uniform float4 _LightColor0; // color of light source

      		// User-specified properties
      		uniform sampler2D _BumpMap;   
	      	uniform float4 _BumpMap_ST;
	      	uniform float4 _Color; 
	      	uniform float4 _SpecColor; 
	      	uniform float _Shininess;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _BumpMap);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_BumpMap, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
