#version 400 core

uniform sampler2D mainTexture;
uniform vec3 lightColor;
uniform float shineDamper;
uniform float reflectivity;
uniform float ambientLight;

in vec2 textureCoordinateForFragmentShader;
in vec3 surfaceNormalForFragmentShader;
in vec3	lightVectorForFragmentShader;	 
in vec3 cameraVectorForFragmentShader;


out vec4 outColor;


vec4 CalculateLighting(vec3 unitNormal, vec3 unitLightVector, float minLight);
vec4 CalculateShining(vec3 unitNormal, vec3 unitLightVector);


void main()
{
	vec4 pixel = texture(mainTexture, textureCoordinateForFragmentShader);

	if(pixel.a > 0.9)
	{
		vec3 unitNormal = normalize(surfaceNormalForFragmentShader);
		vec3 unitLightVector = normalize(lightVectorForFragmentShader);

		outColor = CalculateLighting(unitNormal, unitLightVector, ambientLight) * texture(mainTexture, textureCoordinateForFragmentShader) + CalculateShining(unitNormal, unitLightVector);
	}

}



vec4 CalculateLighting(vec3 unitNormal, vec3 unitLightVector, float minLight)
{
	float dotzDifference = dot(unitNormal, unitLightVector);
	float brightness = max(dotzDifference, minLight);
	vec3 diffuse = brightness * lightColor;

	return vec4(diffuse, 1.0);
}


vec4 CalculateShining(vec3 unitNormal, vec3 unitLightVector)
{
	if(reflectivity == 0) return vec4(0.0, 0.0, 0.0, 1.0);

	vec3 unitCameraVector = normalize(cameraVectorForFragmentShader);

	vec3 reflectedLight = reflect(-unitLightVector, unitNormal);

	float specularFactor = dot(reflectedLight, unitCameraVector);
	specularFactor = max(specularFactor, 0.0);
	float dampedFactor = pow(specularFactor, shineDamper);
	vec3 finalSpecularLight = dampedFactor * reflectivity * lightColor;

	return vec4(finalSpecularLight, 1.0);
}