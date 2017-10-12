// This shader creates a metallic material.
// Created by HangChen Xiong(753057), 10 Oct 2017

Shader "Unlit/PyramidShader"
{
	Properties {
		[Header(RGB)]
	    _Color ("Main Color", Color) = (1,1,1,1)
	    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)

	    [Header(Metallic Settings)]
	    _Metallic ("Metallic", range(0,2)) = 1
	    _BumpMap ("Normalmap", 2D) = "bump" {}
	}
	 
	SubShader {
	    Tags { "RenderType" = "Opaque" }
	    LOD 100
	   
		CGPROGRAM
		#pragma surface surf BlinnPhong2
		 
		sampler2D _BumpMap;
		fixed4 _Color;
		half _Metallic;
		 
		struct SurfaceOutput2 {
		    fixed3 Albedo;
		    fixed3 Normal;
		    half Specular;
		    fixed3 Emission;

		    half3 GlossColor;
		    fixed Gloss;
		    fixed Alpha;
		};

		// Referreced from: https://docs.unity3d.com/Manual/SL-SurfaceShaderLightingExamples.html
		inline fixed4 LightingBlinnPhong2 (SurfaceOutput2 s, fixed3 lightDir, half3 viewDir, fixed atten)
		{
		    half3 h = normalize (lightDir + viewDir);
		    fixed diff = max (0, dot (s.Normal, lightDir));
		    float nh = max (0, dot (s.Normal, h));
		    float spec = pow (nh, s.Specular*96.0) * s.GlossColor;
		    fixed4 c;
		    c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * s.Gloss * spec) * (atten * 2);
		    c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
		    return c;
		}
		 
		inline fixed4 LightingBlinnPhong2_PrePass (SurfaceOutput2 s, half4 light)
		{
			// Define specular value with light and glossiness
		    fixed spec = light.a * s.Gloss;
		    // Define color value with spec 
		    fixed4 c;
		    c.rgb = (s.Albedo * light.rgb + light.rgb * s.GlossColor * _SpecColor.rgb * spec);
		    c.a = s.Alpha + spec * _SpecColor.a;
		    return c;
		}
		 
		 
		struct Input {
		    float2 uv_BumpMap;
		};
		 
		void surf (Input IN, inout SurfaceOutput2 o) {
		    o.Albedo = _Color.rgb;
		    o.Gloss = _Color.a;
		    o.GlossColor = _SpecColor.rgb;
		    o.Alpha = _Color.a;
		    o.Specular = _Metallic;
		    o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	FallBack "Diffuse"
}