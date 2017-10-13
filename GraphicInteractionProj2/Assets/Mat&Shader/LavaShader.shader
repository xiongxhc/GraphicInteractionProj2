// The shader creates a flowing lava effect with Normal map and Rim Emission.
// Created by HangChen Xiong(753057), 10 Oct 2017

Shader "Unlit/LavaShader"
{
	Properties {
		[Header(Texture Maps)]
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_NormalMap("Normal Map", 2D) = "bump" {}
		_BumpScale ("Normal Scale", range(-1,1)) = 1.0

		[Header(Emission Lighting)]
		_EmissionColor ("Rim Color", Color) = (0.2,0.2,0.2,0.0)
      	_EmissionPower ("Rim Power", Range(0.5,8.0)) = 2.0

		[Header(Settings)]
		_MoveSpeedU ("U Movement Speed", Range(-1,1)) = 0.5
		_MoveSpeedV ("V Movement Speed", Range(-1,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" } 
		LOD 100

		CGPROGRAM
		#pragma surface surf Lambert 

		sampler2D _MainTex;
		sampler2D _NormalMap;
		float4 _EmissionColor;
      	float _EmissionPower;

		fixed _MoveSpeedU;
		fixed _MoveSpeedV;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			fixed2 UVposition = IN.uv_MainTex;
			
			fixed MoveU = _MoveSpeedU * _Time;
			fixed MoveV = _MoveSpeedV * _Time;

			// combine U and V movement 
			UVposition += fixed2(MoveU, MoveV);
		
			o.Albedo = tex2D (_MainTex, UVposition).rgb;
			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	o.Emission = _EmissionColor.rgb * pow (rim, _EmissionPower);
		}
		ENDCG
	} 
	FallBack "Diffuse" 
}