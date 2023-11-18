Shader "Custom/DiffuseAlpha" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OpacityTex("Opacity (A)", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows addshadow alpha
        #pragma target 3.0
        sampler2D _MainTex;
        sampler2D _OpacityTex;
        struct Input {
            float2 uv_MainTex;
            float2 uv_OpacityTex;
        };
        fixed4 _Color;
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            half opacity = tex2D(_OpacityTex, IN.uv_OpacityTex).a;
            o.Alpha = opacity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
