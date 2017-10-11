// This shader creates a metallic material with glowing and blending features.
// Created by HangChen Xiong(753057), 10 Oct 2017

Shader "Unlit/PyramidShader"
{
	Properties
	{
		[Header(RGB)]
		_Color ("Diffuse Color", Color) = (1,1,1,1) 
		_NormalMap("Normal Map", 2D) = "bump" {}
      	//_SpecColor ("Specular Color", Color) = (1,1,1,1) 

      	[Header(Metallic Setting)]
      	_Cube ("Cubemap", CUBE) = "" {}
      	_Metallic ("Metallic", range(-1,1)) = 0
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" } 
		LOD 100

		CGPROGRAM
		#pragma surface surf BlinnPhong 

		sampler2D _NormalMap;
		samplerCUBE _Cube;
		fixed4 _Color;
		//fixed4 _SpecColor;
		fixed _Metallic;

		struct Input {
			float2 uv_NormalMap;
		    float3 worldRefl; INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = _Color;
			o.Albedo = c.rgb;
   			o.Gloss = c.a;
   			o.Specular = _Metallic;
   			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
	      	o.Emission = texCUBE (_Cube, IN.worldRefl).rgb;
		}
		ENDCG
	}
	FallBack "Diffuse" 
}
