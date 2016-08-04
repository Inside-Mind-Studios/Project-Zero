 Shader "newShader Simple" {
 	Properties {
 		_MainTex ("Texture", 2D) = "white" {}
 	}
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
      		float2 uv_MainTex;
          	//float4 color : COLOR;
      };
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
         	//o.Albedo = 1;
         	o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }
