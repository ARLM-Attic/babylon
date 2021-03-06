#include "effect.h"
#include "defs.h"
#include "engine.h"

using namespace Babylon;

// Statics
map_t<string, string> Babylon::Effect::ShadersStore;

Babylon::Effect::Effect() {
}

Effect::Ptr Babylon::Effect::New(string baseName, vector_t<VertexBufferKind> attributesNames, vector_t<string> uniformsNames, vector_t<string> samplers, Engine::Ptr engine, string defines, vector_t<string> optionalDefines) {
	auto effect = make_shared<Effect>(Effect());
	effect->_init(baseName, baseName, baseName, attributesNames, uniformsNames, samplers, engine, defines, optionalDefines);
	return effect;
}

Effect::Ptr Babylon::Effect::New(string baseName, string vertex, string fragment, vector_t<VertexBufferKind> attributesNames, vector_t<string> uniformsNames, vector_t<string> samplers, Engine::Ptr engine, string defines, vector_t<string> optionalDefines) {
	auto effect = make_shared<Effect>(Effect());
	effect->_init(baseName, vertex, fragment, attributesNames, uniformsNames, samplers, engine, defines, optionalDefines);
	return effect;
}

void Babylon::Effect::_init(string baseName, string vertex, string fragment, vector_t<VertexBufferKind> attributesNames, vector_t<string> uniformsNames, vector_t<string> samplers, Engine::Ptr engine, string defines, vector_t<string> optionalDefines) {
	this->_engine = engine;
	this->name = baseName;
	this->defines = defines;
	this->_uniformsNames = uniformsNames;
	this->_uniformsNames.insert(end(this->_uniformsNames), begin(samplers), end(samplers));
	this->_samplers = samplers;
	this->_isReady = false;
	this->_compilationError = "";
	this->_attributes = attributesNames;

	// TODO: finish it
	
	this->_loadVertexShader(vertex, [&] (string vertexCode) {
		this->_loadFragmentShader(fragment, [&] (string fragmentCode) {
			this->_prepareEffect(vertexCode, fragmentCode, attributesNames, defines, optionalDefines);
		});
	});   

	// Cache
	this->_valueCache.clear();
};

// Properties
bool Babylon::Effect::isReady() {
	return this->_isReady;
};

IGLProgram::Ptr Babylon::Effect::getProgram() {
	return this->_program;
};

vector_t<VertexBufferKind>& Babylon::Effect::getAttributes() {
	return this->_attributes;
};

int Babylon::Effect::getAttributeLocation(VertexBufferKind kind) {
	return this->_attributeLocations[kind];
};

int Babylon::Effect::getUniformIndex(string uniformName) {
	return find ( begin(this->_uniformsNames), end(this->_uniformsNames), uniformName) - begin(this->_uniformsNames);
};

IGLUniformLocation::Ptr Babylon::Effect::getUniform(string uniformName) {
	return this->_uniforms[getUniformIndex(uniformName)];
};

IGLUniformLocation::Ptr Babylon::Effect::getUniform(int sample) {
	return this->_uniforms[sample];
};

vector_t<string>& Babylon::Effect::getSamplers() {
	return this->_samplers;
};

string Babylon::Effect::getCompilationError() {
	return this->_compilationError;
};

// Methods
void Babylon::Effect::_loadVertexShader(string vertex, CallbackFunc callback) {
	// Is in local store ?
	string key;
	key.append(vertex).append("VertexShader");
	auto value = Effect::ShadersStore[key];
	if (!value.empty()) {
		callback(value);
		return;
	}

	string vertexShaderUrl;

	if (vertex[0] == '.') {
		vertexShaderUrl = vertex;
	} else {
		vertexShaderUrl.append(ShadersRepository).append(vertex);
	}

	// TODO: finish it
	// Vertex shader
	//BABYLON.Tools.LoadFile(vertexShaderUrl + ".vertex.fx", callback);
};

void Babylon::Effect::_loadFragmentShader(string fragment, CallbackFunc callback) {
	// Is in local store ?
	string key;
	key.append(fragment).append("PixelShader");
	auto value = ShadersStore[key];
	if (!value.empty()) {
		callback(value);
		return;
	}

	string fragmentShaderUrl;

	if (fragment[0] == '.') {
		fragmentShaderUrl = fragment;
	} else {
		fragmentShaderUrl.append(ShadersRepository).append(fragment);
	}

	// TODO: finish it
	// Fragment shader
	//BABYLON.Tools.LoadFile(fragmentShaderUrl + ".fragment.fx", callback);
};

void Babylon::Effect::_prepareEffect(string vertexSourceCode, string fragmentSourceCode, vector_t<VertexBufferKind> attributesNames, string defines, vector_t<string> optionalDefines, bool useFallback) {
	
	try {
		auto engine = this->_engine;
		this->_program = engine->createShaderProgram(vertexSourceCode, fragmentSourceCode, defines);

		this->_uniforms = engine->getUniforms(this->_program, this->_uniformsNames);
		this->_attributeLocations = engine->getAttributeLocations(this->_program, attributesNames);

		for (size_t index = 0; index < this->_samplers.size(); index++) {
			auto sampler = this->getUniform(this->_samplers[index]);
			if (!sampler) {
				this->_samplers.erase(begin(this->_samplers) + index, begin(this->_samplers) + index + 1);
				index--;
			}
		}

		engine->bindSamplers(shared_from_this());

		this->_isReady = true;
	} catch (exception ex) {
		if (!useFallback && !optionalDefines.empty()) {
			for (auto optionalDefine : optionalDefines) {
				while (size_t pos = defines.find(optionalDefine) != string::npos)
				{
					defines.replace(pos, optionalDefine.length(), "");
				}
			}
			this->_prepareEffect(vertexSourceCode, fragmentSourceCode, attributesNames, defines, optionalDefines, true);
		} else {
			////console.error("Unable to compile effect: " + this->name);
			////console.error("Defines: " + defines);
			////console.error("Optional defines: " + optionalDefines);
			this->_compilationError = ex.what();
		}
	}
};

void Babylon::Effect::_bindTexture(string channel, IGLTexture::Ptr texture) {
	this->_engine->_bindTexture(find(begin(this->_samplers), end(this->_samplers), channel) - begin(this->_samplers), texture);
};

void Babylon::Effect::setTexture(string channel, Texture::Ptr texture) {
	this->_engine->setTexture(find(begin(this->_samplers), end(this->_samplers), channel) - begin(this->_samplers), texture);
};

void Babylon::Effect::setTextureFromPostProcess(string channel, PostProcess::Ptr postProcess) {
	this->_engine->setTextureFromPostProcess(find(begin(this->_samplers), end(this->_samplers), channel) - begin(this->_samplers), postProcess);
};

void Babylon::Effect::_cacheFloat2(string uniformName, float x, float y) {
	if (!this->_valueCache.count(uniformName)) {
		this->_valueCache[uniformName] = Float32Array(2);
	}

	this->_valueCache[uniformName][0] = x;
	this->_valueCache[uniformName][1] = y;
};

void Babylon::Effect::_cacheFloat3(string uniformName, float x, float y, float z) {
	if (!this->_valueCache.count(uniformName)) {
		this->_valueCache[uniformName] = Float32Array(3);
	}

	this->_valueCache[uniformName][0] = x;
	this->_valueCache[uniformName][1] = y;
	this->_valueCache[uniformName][2] = z;
};

void Babylon::Effect::_cacheFloat4(string uniformName, float x, float y, float z, float w) {
	if (!this->_valueCache.count(uniformName)) {
		this->_valueCache[uniformName] =  Float32Array(4);
	}

	this->_valueCache[uniformName][0] = x;
	this->_valueCache[uniformName][1] = y;
	this->_valueCache[uniformName][2] = z;
	this->_valueCache[uniformName][3] = w;
};

void Babylon::Effect::setMatrices(string uniformName, Float32Array matrices) {
	this->_engine->setMatrices(this->getUniform(uniformName), matrices);
};

void Babylon::Effect::setMatrix(string uniformName, Matrix::Ptr matrix) {
	this->_engine->setMatrix(this->getUniform(uniformName), matrix);
};

void Babylon::Effect::setFloat(string uniformName, float value) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == value)
		return;

	this->_valueCache[uniformName] = Float32Array(1);
	this->_valueCache[uniformName][0] = value;

	this->_engine->setFloat(this->getUniform(uniformName), value);
};

void Babylon::Effect::setBool(string uniformName, GLboolean _bool) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == _bool)
		return;

	this->_valueCache[uniformName] = Float32Array(1);
	this->_valueCache[uniformName][0] = _bool;

	this->_engine->setBool(this->getUniform(uniformName), _bool);
};

void Babylon::Effect::setVector2(string uniformName, Vector2::Ptr vector2) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == vector2->x && this->_valueCache[uniformName][1] == vector2->y)
		return;

	this->_cacheFloat2(uniformName, vector2->x, vector2->y);
	this->_engine->setFloat2(this->getUniform(uniformName), vector2->x, vector2->y);
};

void Babylon::Effect::setFloat2(string uniformName, float x, float y) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == x && this->_valueCache[uniformName][1] == y)
		return;

	this->_cacheFloat2(uniformName, x, y);
	this->_engine->setFloat2(this->getUniform(uniformName), x, y);
};

void Babylon::Effect::setVector3(string uniformName, Vector3::Ptr vector3) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == vector3->x && this->_valueCache[uniformName][1] == vector3->y && this->_valueCache[uniformName][2] == vector3->z)
		return;

	this->_cacheFloat3(uniformName, vector3->x, vector3->y, vector3->z);

	this->_engine->setFloat3(this->getUniform(uniformName), vector3->x, vector3->y, vector3->z);
};

void Babylon::Effect::setFloat3(string uniformName, float x, float y, float z) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == x && this->_valueCache[uniformName][1] == y && this->_valueCache[uniformName][2] == z)
		return;

	this->_cacheFloat3(uniformName, x, y, z);
	this->_engine->setFloat3(this->getUniform(uniformName), x, y, z);
};

void Babylon::Effect::setFloat4(string uniformName, float x, float y, float z, float w) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == x && this->_valueCache[uniformName][1] == y && this->_valueCache[uniformName][2] == z && this->_valueCache[uniformName][3] == w)
		return;

	this->_cacheFloat4(uniformName, x, y, z, w);
	this->_engine->setFloat4(this->getUniform(uniformName), x, y, z, w);
};

void Babylon::Effect::setColor3(string uniformName, Color3::Ptr color3) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == color3->r && this->_valueCache[uniformName][1] == color3->g && this->_valueCache[uniformName][2] == color3->b)
		return;

	this->_cacheFloat3(uniformName, color3->r, color3->g, color3->b);
	this->_engine->setColor3(this->getUniform(uniformName), color3);
};

void Babylon::Effect::setColor4(string uniformName, Color3::Ptr color3, float alpha) {
	if (this->_valueCache.count(uniformName) && this->_valueCache[uniformName][0] == color3->r && this->_valueCache[uniformName][1] == color3->g && this->_valueCache[uniformName][2] == color3->b && this->_valueCache[uniformName][3] == alpha)
		return;

	this->_cacheFloat4(uniformName, color3->r, color3->g, color3->b, alpha);
	this->_engine->setColor4(this->getUniform(uniformName), color3, alpha);
};