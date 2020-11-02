Shader "Custom/SnowShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Normal ("Normal", 2D) = "white" {}
        _Height ("Height", 2D) = "white" {}
        _Occlusion ("Occlusion", 2D) = "white" {}
        //Add Emission to simulate snow reflection
        [HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
    }
    SubShader
    {
        // Phong Shader from the tutorial without Speculation
        Pass {
            // For our sun light source 
            Tags { "LightMode" = "ForwardBase"}

            CGPROGRAM 
            #pragma vertex vert
            #pragma fragment frag 

            // Importing light, camera information 
            #include "UnityCG.cginc"

            // UnityCG 
            uniform float4 _LightColor0;

            //Used for texture
            sampler2D _Tex; 
            //For tiling 
            float4 _Tex_ST; 

            uniform float4 _Colour;
            uniform float4 _SpecColour;
            uniform float _Shininess;

            struct vertIn
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; 
                float2 uv : TEXCOORD0;
                
            };

            struct vertOut
            {
                float4 pos : POSITION;
                float3 normal : NORMAL; 
                float2 uv: TEXCOORD0; 
                float4 posWorld : TEXCOORD1; 
            }; 

            // Transforming the position of vertices 
            vertOut vert(vertIn v)
            {
                vertOut o; 
                // Calculating the world positon of our object 
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                //Calculating the normal 
                o.normal = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
                // Updating the position 
                o.pos = UnityObjectToClipPos(v.vertex);
                // UVs for mapping the texture 
                o.uv = TRANSFORM_TEX(v.uv, _Tex);
                return o; 
            }

            // Updating the colour 
            fixed4 frag(vertOut i) : COLOR 
            {
                float3 normalDirection = normalize(i.normal);
                // Our eye will be the main camera 
                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

                float3 vert2LightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
                float oneOverDistance = 1.0 / length(vert2LightSource);
                float3 lightDirection = _WorldSpaceLightPos0.xyz - i.posWorld.xyz * _WorldSpaceLightPos0.w;

                //Attenunation 
                float attenuation = lerp(1.0, oneOverDistance, _WorldSpaceLightPos0.w); 

                // Ambient component 
                float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Colour.rgb;
                // Diffuse component 
                float3 diffuseReflection = _LightColor0.rgb * _Colour.rgb * 
                max(0.0, dot(normalDirection, lightDirection)) * attenuation;   

                // Calculating colour based on the three components 
                float3 color = (ambientLighting + diffuseReflection) * tex2D(_Tex, i.uv);
                return float4(color, 1.0);
            }
            
            ENDCG
            
        }
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _SnowTex;
        sampler2D _Normal;
        sampler2D _Height;
        sampler2D _Occlusion;
        fixed4 _EmissionColor;

        struct Input
        {
            float2 uv_MainTex;
        };
        fixed4 _Color;


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input i, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, i.uv_MainTex) * _Color;
            fixed4 snowColor = tex2D (_SnowTex, i.uv_MainTex) * _Color;
            fixed4 occlusion = tex2D (_Occlusion, i.uv_MainTex);
            o.Albedo = c.rgb;
            o.Occlusion = occlusion.r;
            //Setting Emission to give a tint of blue
            o.Emission = c.rgb * tex2D(_MainTex, i.uv_MainTex).a * _EmissionColor;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}