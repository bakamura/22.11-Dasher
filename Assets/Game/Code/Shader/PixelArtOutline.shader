Shader "Sprites/PixelArtOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color",  Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Previewtype" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        ZWrite Off
        Cull Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            // Try making GLSL latter
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct fragData {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };


            fragData vert (appdata_base vData) {
                fragData fData;
                fData.vertex = UnityObjectToClipPos(vData.vertex);
                fData.uv = vData.texcoord;
                return fData;
            }

            float4 _MainTex_TexelSize;
            float4 _OutlineColor;

            fixed4 frag (fragData fData) : COLOR {
                half4 color = tex2D(_MainTex, fData.uv); 
                color.rgb *= color.a;

                fixed upA = tex2D(_MainTex, fData.uv + fixed2(0, _MainTex_TexelSize.y)).a;
                fixed rightA = tex2D(_MainTex, fData.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
                fixed leftA = tex2D(_MainTex, fData.uv - fixed2(_MainTex_TexelSize.x, 0)).a;
                fixed downA = tex2D(_MainTex, fData.uv - fixed2(0, _MainTex_TexelSize.y)).a;
                if(color.a < 1 && upA + rightA + leftA + downA > 0) color = _OutlineColor;

                return color;
            }

            ENDCG
        }
    }
}
