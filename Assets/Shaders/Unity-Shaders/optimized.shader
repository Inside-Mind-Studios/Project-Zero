// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Unity-Shaders/5 - Optimized" {
	Properties{
		_Color("Color Tint", Color) = (1,1,1,1)
		_MainTex("Diffuse Texture", 2D) = "white" {}
		_BumpMap("Normal Texture", 2D) = "bump" {}
		_BumpDepth("Bump Depth", Range(0.0, 1.0)) = 1.0
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower("Rim Power", Range(0.1, 10)) = 3.0
		_EmitMap("Emission Texture", 2D) = "black" {}
		_EmitStrength("Emission Strength", Range(0.0, 1.0)) = 1.0
	}
	SubShader{
		Pass{
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag

			//user defined variables
			uniform fixed4 _Color;
			uniform fixed4 _SpecColor;
			uniform fixed4 _RimColor;
			uniform sampler2D _MainTex;
			uniform sampler2D _EmitMap;
			uniform sampler2D _BumpMap;
			uniform half4 _MainTex_ST;
			uniform half4 _BumpMap_ST;
			uniform half4 _EmitMap_ST;
			uniform half _Shininess;
			uniform half _RimPower;
			uniform fixed _BumpDepth;
			uniform fixed _EmitStrength;

			//Unity defined variables
			uniform half4 _LightColor0;

			//base input struct
			struct vertexInput
			{
				half4 vertex : POSITION;
				half3 normal : NORMAL;
				half4 texcoord : TEXCOORD0;
				half4 tangent : TANGENT;
			};
			struct vertexOutput
			{
				half4 pos : SV_POSITION;
				half4 tex : TEXCOORD0;
				fixed4 lightDir : TEXCOORD1;
				fixed3 viewDir : TEXCOORD2;
				fixed3 normalWorld : TEXCOORD3;
				fixed3 tangentWorld : TEXCOORD4;
				fixed3 binormalWorld : TEXCOORD5;
			};

			//vertex function
			vertexOutput vert(vertexInput IN)
			{
				vertexOutput OUT;

				OUT.normalWorld = normalize(mul(half4(IN.normal, 0.0), unity_WorldToObject).xyz);
				OUT.tangentWorld = normalize(mul(unity_ObjectToWorld, IN.tangent).xyz);
				OUT.binormalWorld = normalize(cross(OUT.normalWorld, OUT.tangentWorld) * IN.tangent.w);

				half4 posWorld = mul(unity_ObjectToWorld, IN.vertex);
				OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.tex = IN.texcoord;

				OUT.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
				half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;

				OUT.lightDir = fixed4(
					normalize(lerp(_WorldSpaceLightPos0.xyz, fragmentToLightSource, _WorldSpaceLightPos0.w)), 
					lerp(1.0, 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);

				return OUT;
			}

			//fragment function
			fixed4 frag(vertexOutput IN) : COLOR
			{
				//Texture Maps
				fixed4 tex = tex2D(_MainTex, IN.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed4 texN = tex2D(_BumpMap, IN.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);
				fixed4 texE = tex2D(_EmitMap, IN.tex.xy * _EmitMap_ST.xy + _EmitMap_ST.zw);

				//unpackNormal function
				fixed3 localCoords = fixed3(2.0 * texN.ag - float2(1.0, 1.0), _BumpDepth);

				//normal transpose matrix
				half3x3 local2WorldTranspose = float3x3(
					IN.tangentWorld,
					IN.binormalWorld,
					IN.normalWorld
				);

				//calculate normal direction
				fixed3 normalDir = normalize(mul(localCoords, local2WorldTranspose));

				//Lighting
				//dot product
				fixed nDotL = saturate(dot(normalDir, IN.lightDir.xyz));
				//float diffuse = saturate(dot(normalDir, IN.lightDir.xyz));
				fixed3 diffuseReflect = IN.lightDir.w * _LightColor0.xyz * nDotL;
				fixed3 specularReflect = diffuseReflect * _SpecColor.xyz * pow(saturate(dot(reflect(-IN.lightDir.xyz, normalDir), IN.viewDir)), _Shininess);

				//Rim Lighting
				fixed rim = 1 - saturate(dot(IN.viewDir, normalDir));
				fixed3 rimLighting = nDotL * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower);

				fixed3 lightFinal = UNITY_LIGHTMODEL_AMBIENT + diffuseReflect + (specularReflect * tex.a) + rimLighting + (texE.xyz * _EmitStrength);

				return fixed4(tex.xyz * lightFinal * _Color.xyz, 1.0);
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
			uniform fixed4 _Color;
		uniform fixed4 _SpecColor;
		uniform fixed4 _RimColor;
		uniform sampler2D _MainTex;
		uniform sampler2D _EmitMap;
		uniform sampler2D _BumpMap;
		uniform half4 _MainTex_ST;
		uniform half4 _BumpMap_ST;
		uniform half4 _EmitMap_ST;
		uniform half _Shininess;
		uniform half _RimPower;
		uniform fixed _BumpDepth;
		uniform fixed _EmitStrength;

		//Unity defined variables
		uniform half4 _LightColor0;

		//base input struct
		struct vertexInput
		{
			half4 vertex : POSITION;
			half3 normal : NORMAL;
			half4 texcoord : TEXCOORD0;
			half4 tangent : TANGENT;
		};
		struct vertexOutput
		{
			half4 pos : SV_POSITION;
			half4 tex : TEXCOORD0;
			fixed4 lightDir : TEXCOORD1;
			fixed3 viewDir : TEXCOORD2;
			fixed3 normalWorld : TEXCOORD3;
			fixed3 tangentWorld : TEXCOORD4;
			fixed3 binormalWorld : TEXCOORD5;
		};

		//vertex function
		vertexOutput vert(vertexInput IN)
		{
			vertexOutput OUT;

			OUT.normalWorld = normalize(mul(half4(IN.normal, 0.0), unity_WorldToObject).xyz);
			OUT.tangentWorld = normalize(mul(unity_ObjectToWorld, IN.tangent).xyz);
			OUT.binormalWorld = normalize(cross(OUT.normalWorld, OUT.tangentWorld) * IN.tangent.w);

			half4 posWorld = mul(unity_ObjectToWorld, IN.vertex);
			OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
			OUT.tex = IN.texcoord;

			OUT.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
			half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;

			OUT.lightDir = fixed4(
				normalize(lerp(_WorldSpaceLightPos0.xyz, fragmentToLightSource, _WorldSpaceLightPos0.w)),
				lerp(1.0, 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);

			return OUT;
		}

		//fragment function
		fixed4 frag(vertexOutput IN) : COLOR
		{
			//Texture Maps
			fixed4 tex = tex2D(_MainTex, IN.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
		fixed4 texN = tex2D(_BumpMap, IN.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);
		fixed4 texE = tex2D(_EmitMap, IN.tex.xy * _EmitMap_ST.xy + _EmitMap_ST.zw);

		//unpackNormal function
		fixed3 localCoords = fixed3(2.0 * texN.ag - float2(1.0, 1.0), _BumpDepth);

		//normal transpose matrix
		half3x3 local2WorldTranspose = float3x3(
			IN.tangentWorld,
			IN.binormalWorld,
			IN.normalWorld
			);

		//calculate normal direction
		fixed3 normalDir = normalize(mul(localCoords, local2WorldTranspose));

		//Lighting
		//dot product
		fixed nDotL = saturate(dot(normalDir, IN.lightDir.xyz));
		//float diffuse = saturate(dot(normalDir, IN.lightDir.xyz));
		fixed3 diffuseReflect = IN.lightDir.w * _LightColor0.xyz * nDotL;
		fixed3 specularReflect = diffuseReflect * _SpecColor.xyz * pow(saturate(dot(reflect(-IN.lightDir.xyz, normalDir), IN.viewDir)), _Shininess);

		//Rim Lighting
		fixed rim = 1 - saturate(dot(IN.viewDir, normalDir));
		fixed3 rimLighting = nDotL * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower);

		fixed3 lightFinal = diffuseReflect + (specularReflect * tex.a) + rimLighting;

		return fixed4(tex.xyz * lightFinal * _Color.xyz, 1.0);
		}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
