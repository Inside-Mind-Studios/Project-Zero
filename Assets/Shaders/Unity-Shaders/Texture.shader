// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Unity-Shaders/2 - Texture" {
	Properties {
		_Color ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower("Rim Power", Range(0.1, 10)) = 3.0
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag

			//user defined variables
			uniform float4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;

			//Unity defined variables
			uniform float4 _LightColor0;

			//base input struct
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
			};

			//vertex function
			vertexOutput vert(vertexInput IN)
			{
				vertexOutput OUT;

				OUT.posWorld = mul(unity_ObjectToWorld, IN.vertex);
				OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.normalDir = normalize(mul(float4(IN.normal, 0.0), unity_WorldToObject).xyz);
				OUT.tex = IN.texcoord;

				return OUT;
			}

			//fragment function
			float4 frag(vertexOutput IN) : COLOR
			{
				float3 normalDir = IN.normalDir;
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - IN.posWorld.xyz);
				float3 lightDir;
				float atten;

				if (_WorldSpaceLightPos0.w == 0) {
					atten = 1.0;
					lightDir = normalize(_WorldSpaceLightPos0.xyz);
				} else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - IN.posWorld.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1 / distance;
					lightDir = normalize(fragmentToLightSource);
				}

				//Lighting
				float diffuse = saturate(dot(normalDir, lightDir));
				float3 diffuseReflect = atten * _LightColor0.xyz * diffuse;
				float specular = pow(saturate(dot(reflect(-lightDir, normalDir), viewDir)), _Shininess);
				float3 specularReflect = diffuseReflect * _SpecColor.xyz * specular;

				//Rim Lighting
				float rim = 1 - saturate(dot(viewDir, normalDir));
				float3 rimLighting = saturate(dot(normalDir, lightDir)) * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower);

				float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT + diffuseReflect + specularReflect + rimLighting;

				//Texture Maps
				float4 tex = tex2D(_MainTex, IN.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				return float4(tex.xyz * lightFinal * _Color.xyz, 1.0);
			}

			ENDCG
		}
		Pass{
			Tags{ "LightMode" = "ForwardAdd" }
			Blend One One
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag

			//user defined variables
			uniform float4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;

			//Unity defined variables
			uniform float4 _LightColor0;

			//base input struct
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
			};

			//vertex function
			vertexOutput vert(vertexInput IN)
			{
				vertexOutput OUT;

				OUT.posWorld = mul(unity_ObjectToWorld, IN.vertex);
				OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.normalDir = normalize(mul(float4(IN.normal, 0.0), unity_WorldToObject).xyz);
				OUT.tex = IN.texcoord;

				return OUT;
			}

			//fragment function
			float4 frag(vertexOutput IN) : COLOR
			{
				float3 normalDir = IN.normalDir;
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - IN.posWorld.xyz);
				float3 lightDir;
				float atten;

				if (_WorldSpaceLightPos0.w == 0) {
					atten = 1.0;
					lightDir = normalize(_WorldSpaceLightPos0.xyz);
				}
				else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - IN.posWorld.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1 / distance;
					lightDir = normalize(fragmentToLightSource);
				}

				//Lighting
				float diffuse = saturate(dot(normalDir, lightDir));
				float3 diffuseReflect = atten * _LightColor0.xyz * diffuse;
				float specular = pow(saturate(dot(reflect(-lightDir, normalDir), viewDir)), _Shininess);
				float3 specularReflect = diffuseReflect * _SpecColor.xyz * specular;

				//Rim Lighting
				float rim = 1 - saturate(dot(viewDir, normalDir));
				float3 rimLighting = saturate(dot(normalDir, lightDir)) * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower);

				float3 lightFinal = diffuseReflect + specularReflect + rimLighting;

				//Texture Maps
				float4 tex = tex2D(_MainTex, IN.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				return float4(lightFinal * _Color.xyz, 1.0);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
