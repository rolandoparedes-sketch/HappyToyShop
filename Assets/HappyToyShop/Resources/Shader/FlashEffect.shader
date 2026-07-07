Shader "Custom/URP/FlashEffect"
{
    Properties
    {
        [Header(Colores)]
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        [HDR] _FlashColor ("Flash Color", Color) = (1,0,0,1)

        [Header(Configuracion del Flash)]
        _EnableFlash ("Enable Flash (0 = off, 1 = on)", Float) = 0
        _FlashFrequency ("Flash Frequency (ciclos por segundo)", Range(0.1, 20)) = 2.0
        _FlashIntensity ("Flash Intensity", Range(0, 1)) = 1.0

        [Header(Textura opcional)]
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }
        LOD 100

        Pass
        {
            Name "ForwardUnlit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BaseColor;
                float4 _FlashColor;
                float _EnableFlash;
                float _FlashFrequency;
                float _FlashIntensity;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                half4 col = texColor * _BaseColor;

                if (_EnableFlash > 0.5)
                {
                    // Onda senoidal entre 0 y 1 segun la frecuencia elegida
                    float wave = sin(_Time.y * _FlashFrequency * 2 * PI) * 0.5 + 0.5;
                    float flashAmount = wave * _FlashIntensity;
                    col.rgb = lerp(col.rgb, _FlashColor.rgb, flashAmount);
                }

                return col;
            }
            ENDHLSL
        }
    }

    FallBack "Universal Render Pipeline/Unlit"
}
