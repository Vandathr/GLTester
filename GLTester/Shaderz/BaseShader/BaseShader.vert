#version 400 core

uniform mat4 projectionMatrix;
uniform mat4 transformationMatrix;
uniform mat4 viewMatrix;

uniform vec3 lightPosition;


in vec3 AttributePosition;
in vec4 AttributeColor;
in vec2 AttributeTextureCoordinate;
in vec3 AttributeNormal;

out vec2 textureCoordinateForFragmentShader;
out vec3 surfaceNormalForFragmentShader;
out vec3 lightVectorForFragmentShader;
out vec3 cameraVectorForFragmentShader;




void main()
{

	vec4 worldPosition = transformationMatrix * vec4(AttributePosition, 1.0);
	gl_Position = projectionMatrix * viewMatrix * worldPosition;

	textureCoordinateForFragmentShader = AttributeTextureCoordinate;

	surfaceNormalForFragmentShader = (transformationMatrix * vec4(AttributeNormal, 0.0)).xyz;

	lightVectorForFragmentShader = lightPosition - worldPosition.xyz;

	cameraVectorForFragmentShader = (inverse(viewMatrix) * vec4(0.0, 0.0, 0.0, 1.0)).xyz - worldPosition.xyz;
}