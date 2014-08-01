using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web;
namespace BABYLON {
    public partial class StandardMaterial: Material {
        public BaseTexture diffuseTexture;
        public BaseTexture ambientTexture;
        public BaseTexture opacityTexture;
        public BaseTexture reflectionTexture;
        public BaseTexture emissiveTexture;
        public BaseTexture specularTexture;
        public BaseTexture bumpTexture;
        public BABYLON.Color3 ambientColor = new BABYLON.Color3(0, 0, 0);
        public BABYLON.Color3 diffuseColor = new BABYLON.Color3(1, 1, 1);
        public BABYLON.Color3 specularColor = new BABYLON.Color3(1, 1, 1);
        public double specularPower = 64;
        public BABYLON.Color3 emissiveColor = new BABYLON.Color3(0, 0, 0);
        public bool useAlphaFromDiffuseTexture = false;
        private any _cachedDefines = null;
        private BABYLON.SmartArray < RenderTargetTexture > _renderTargets = new BABYLON.SmartArray < RenderTargetTexture > (16);
        private BABYLON.Matrix _worldViewProjectionMatrix = BABYLON.Matrix.Zero();
        private BABYLON.Color3 _globalAmbientColor = new BABYLON.Color3(0, 0, 0);
        private BABYLON.Color3 _baseColor = new BABYLON.Color3();
        private BABYLON.Color3 _scaledDiffuse = new BABYLON.Color3();
        private BABYLON.Color3 _scaledSpecular = new BABYLON.Color3();
        private double _renderId;
        public StandardMaterial(string name, Scene scene): base(name, scene) {
            this.getRenderTargetTextures = () => {
                this._renderTargets.reset();
                if (this.reflectionTexture && this.reflectionTexture.isRenderTarget) {
                    this._renderTargets.push(this.reflectionTexture);
                }
                return this._renderTargets;
            };
        }
        void maxSimultaneousLights4;
        public virtual bool needAlphaBlending() {
            return (this.alpha < 1.0) || (this.opacityTexture != null) || this._shouldUseAlphaFromDiffuseTexture();
        }
        public virtual bool needAlphaTesting() {
            return this.diffuseTexture != null && this.diffuseTexture.hasAlpha && !this.diffuseTexture.getAlphaFromRGB;
        }
        private bool _shouldUseAlphaFromDiffuseTexture() {
            return this.diffuseTexture != null && this.diffuseTexture.hasAlpha && this.useAlphaFromDiffuseTexture;
        }
        public virtual BaseTexture getAlphaTestTexture() {
            return this.diffuseTexture;
        }
        public virtual bool isReady(AbstractMesh mesh = null, bool useInstances = false) {
            if (this.checkReadyOnlyOnce) {
                if (this._wasPreviouslyReady) {
                    return true;
                }
            }
            var scene = this.getScene();
            if (!this.checkReadyOnEveryCall) {
                if (this._renderId == scene.getRenderId()) {
                    return true;
                }
            }
            var engine = scene.getEngine();
            var defines = new Array < object > ();
            var optionalDefines = new Array < string > ();
            if (scene.texturesEnabled) {
                if (this.diffuseTexture && BABYLON.StandardMaterial.DiffuseTextureEnabled) {
                    if (!this.diffuseTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define DIFFUSE");
                    }
                }
                if (this.ambientTexture && BABYLON.StandardMaterial.AmbientTextureEnabled) {
                    if (!this.ambientTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define AMBIENT");
                    }
                }
                if (this.opacityTexture && BABYLON.StandardMaterial.OpacityTextureEnabled) {
                    if (!this.opacityTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define OPACITY");
                        if (this.opacityTexture.getAlphaFromRGB) {
                            defines.push("#define OPACITYRGB");
                        }
                    }
                }
                if (this.reflectionTexture && BABYLON.StandardMaterial.ReflectionTextureEnabled) {
                    if (!this.reflectionTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define REFLECTION");
                    }
                }
                if (this.emissiveTexture && BABYLON.StandardMaterial.EmissiveTextureEnabled) {
                    if (!this.emissiveTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define EMISSIVE");
                    }
                }
                if (this.specularTexture && BABYLON.StandardMaterial.SpecularTextureEnabled) {
                    if (!this.specularTexture.isReady()) {
                        return false;
                    } else {
                        defines.push("#define SPECULAR");
                        optionalDefines.push(defines[defines.Length - 1]);
                    }
                }
            }
            if (scene.getEngine().getCaps().standardDerivatives && this.bumpTexture && BABYLON.StandardMaterial.BumpTextureEnabled) {
                if (!this.bumpTexture.isReady()) {
                    return false;
                } else {
                    defines.push("#define BUMP");
                    optionalDefines.push(defines[defines.Length - 1]);
                }
            }
            if (scene.clipPlane) {
                defines.push("#define CLIPPLANE");
            }
            if (engine.getAlphaTesting()) {
                defines.push("#define ALPHATEST");
            }
            if (this._shouldUseAlphaFromDiffuseTexture()) {
                defines.push("#define ALPHAFROMDIFFUSE");
            }
            if (scene.fogMode != BABYLON.Scene.FOGMODE_NONE) {
                defines.push("#define FOG");
                optionalDefines.push(defines[defines.Length - 1]);
            }
            var shadowsActivated = false;
            var lightIndex = 0;
            if (scene.lightsEnabled) {
                for (var index = 0; index < scene.lights.Length; index++) {
                    var light = scene.lights[index];
                    if (!light.isEnabled()) {
                        continue;
                    }
                    if (light._excludedMeshesIds.Length > 0) {
                        for (var excludedIndex = 0; excludedIndex < light._excludedMeshesIds.Length; excludedIndex++) {
                            var excludedMesh = scene.getMeshByID(light._excludedMeshesIds[excludedIndex]);
                            if (excludedMesh) {
                                light.excludedMeshes.push(excludedMesh);
                            }
                        }
                        light._excludedMeshesIds = new Array < object > ();
                    }
                    if (mesh && light.excludedMeshes.indexOf(mesh) != -1) {
                        continue;
                    }
                    defines.push("#define LIGHT" + lightIndex);
                    if (lightIndex > 0) {
                        optionalDefines.push(defines[defines.Length - 1]);
                    }
                    var type;
                    if (light is BABYLON.SpotLight) {
                        type = "#define SPOTLIGHT" + lightIndex;
                    } else
                    if (light is BABYLON.HemisphericLight) {
                        type = "#define HEMILIGHT" + lightIndex;
                    } else {
                        type = "#define POINTDIRLIGHT" + lightIndex;
                    }
                    defines.push(type);
                    if (lightIndex > 0) {
                        optionalDefines.push(defines[defines.Length - 1]);
                    }
                    var shadowGenerator = light.getShadowGenerator();
                    if (mesh && mesh.receiveShadows && shadowGenerator) {
                        defines.push("#define SHADOW" + lightIndex);
                        if (lightIndex > 0) {
                            optionalDefines.push(defines[defines.Length - 1]);
                        }
                        if (!shadowsActivated) {
                            defines.push("#define SHADOWS");
                            shadowsActivated = true;
                        }
                        if (shadowGenerator.useVarianceShadowMap) {
                            defines.push("#define SHADOWVSM" + lightIndex);
                            if (lightIndex > 0) {
                                optionalDefines.push(defines[defines.Length - 1]);
                            }
                        }
                        if (shadowGenerator.usePoissonSampling) {
                            defines.push("#define SHADOWPCF" + lightIndex);
                            if (lightIndex > 0) {
                                optionalDefines.push(defines[defines.Length - 1]);
                            }
                        }
                    }
                    lightIndex++;
                    if (lightIndex == maxSimultaneousLights)
                        break;
                }
            }
            var attribs = new Array < object > (BABYLON.VertexBufferKind.PositionKind, BABYLON.VertexBufferKind.NormalKind);
            if (mesh) {
                if (mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.UVKind)) {
                    attribs.push(BABYLON.VertexBufferKind.UVKind);
                    defines.push("#define UV1");
                }
                if (mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.UV2Kind)) {
                    attribs.push(BABYLON.VertexBufferKind.UV2Kind);
                    defines.push("#define UV2");
                }
                if (mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.ColorKind)) {
                    attribs.push(BABYLON.VertexBufferKind.ColorKind);
                    defines.push("#define VERTEXCOLOR");
                }
                if (mesh.skeleton && mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.MatricesIndicesKind) && mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.MatricesWeightsKind)) {
                    attribs.push(BABYLON.VertexBufferKind.MatricesIndicesKind);
                    attribs.push(BABYLON.VertexBufferKind.MatricesWeightsKind);
                    defines.push("#define BONES");
                    defines.push("#define BonesPerMesh " + (mesh.skeleton.bones.Length + 1));
                    defines.push("#define BONES4");
                    optionalDefines.push(defines[defines.Length - 1]);
                }
                if (useInstances) {
                    defines.push("#define INSTANCES");
                    attribs.push("world0");
                    attribs.push("world1");
                    attribs.push("world2");
                    attribs.push("world3");
                }
            }
            var join = defines.join("\\n");
            if (this._cachedDefines != join) {
                this._cachedDefines = join;
                var shaderName = "default";
                if (!scene.getEngine().getCaps().standardDerivatives) {
                    shaderName = "legacydefault";
                }
                this._effect = scene.getEngine().createEffect(shaderName, attribs, new Array < object > ("world", "view", "viewProjection", "vEyePosition", "vLightsType", "vAmbientColor", "vDiffuseColor", "vSpecularColor", "vEmissiveColor", "vLightData0", "vLightDiffuse0", "vLightSpecular0", "vLightDirection0", "vLightGround0", "lightMatrix0", "vLightData1", "vLightDiffuse1", "vLightSpecular1", "vLightDirection1", "vLightGround1", "lightMatrix1", "vLightData2", "vLightDiffuse2", "vLightSpecular2", "vLightDirection2", "vLightGround2", "lightMatrix2", "vLightData3", "vLightDiffuse3", "vLightSpecular3", "vLightDirection3", "vLightGround3", "lightMatrix3", "vFogInfos", "vFogColor", "vDiffuseInfos", "vAmbientInfos", "vOpacityInfos", "vReflectionInfos", "vEmissiveInfos", "vSpecularInfos", "vBumpInfos", "mBones", "vClipPlane", "diffuseMatrix", "ambientMatrix", "opacityMatrix", "reflectionMatrix", "emissiveMatrix", "specularMatrix", "bumpMatrix", "darkness0", "darkness1", "darkness2", "darkness3"), new Array < object > ("diffuseSampler", "ambientSampler", "opacitySampler", "reflectionCubeSampler", "reflection2DSampler", "emissiveSampler", "specularSampler", "bumpSampler", "shadowSampler0", "shadowSampler1", "shadowSampler2", "shadowSampler3"), join, optionalDefines, this.onCompiled, this.onError);
            }
            if (!this._effect.isReady()) {
                return false;
            }
            this._renderId = scene.getRenderId();
            this._wasPreviouslyReady = true;
            return true;
        }
        public virtual void unbind() {
            if (this.reflectionTexture && this.reflectionTexture.isRenderTarget) {
                this._effect.setTexture("reflection2DSampler", null);
            }
        }
        public virtual void bindOnlyWorldMatrix(Matrix world) {
            this._effect.setMatrix("world", world);
        }
        public virtual void bind(Matrix world, Mesh mesh) {
            var scene = this.getScene();
            this._baseColor.copyFrom(this.diffuseColor);
            this.bindOnlyWorldMatrix(world);
            this._effect.setMatrix("viewProjection", scene.getTransformMatrix());
            if (mesh.skeleton && mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.MatricesIndicesKind) && mesh.isVerticesDataPresent(BABYLON.VertexBufferKind.MatricesWeightsKind)) {
                this._effect.setMatrices("mBones", mesh.skeleton.getTransformMatrices());
            }
            if (this.diffuseTexture && BABYLON.StandardMaterial.DiffuseTextureEnabled) {
                this._effect.setTexture("diffuseSampler", this.diffuseTexture);
                this._effect.setFloat2("vDiffuseInfos", this.diffuseTexture.coordinatesIndex, this.diffuseTexture.level);
                this._effect.setMatrix("diffuseMatrix", this.diffuseTexture.getTextureMatrix());
                this._baseColor.copyFromFloats(1, 1, 1);
            }
            if (this.ambientTexture && BABYLON.StandardMaterial.AmbientTextureEnabled) {
                this._effect.setTexture("ambientSampler", this.ambientTexture);
                this._effect.setFloat2("vAmbientInfos", this.ambientTexture.coordinatesIndex, this.ambientTexture.level);
                this._effect.setMatrix("ambientMatrix", this.ambientTexture.getTextureMatrix());
            }
            if (this.opacityTexture && BABYLON.StandardMaterial.OpacityTextureEnabled) {
                this._effect.setTexture("opacitySampler", this.opacityTexture);
                this._effect.setFloat2("vOpacityInfos", this.opacityTexture.coordinatesIndex, this.opacityTexture.level);
                this._effect.setMatrix("opacityMatrix", this.opacityTexture.getTextureMatrix());
            }
            if (this.reflectionTexture && BABYLON.StandardMaterial.ReflectionTextureEnabled) {
                if (this.reflectionTexture.isCube) {
                    this._effect.setTexture("reflectionCubeSampler", this.reflectionTexture);
                } else {
                    this._effect.setTexture("reflection2DSampler", this.reflectionTexture);
                }
                this._effect.setMatrix("reflectionMatrix", this.reflectionTexture.getReflectionTextureMatrix());
                this._effect.setFloat3("vReflectionInfos", this.reflectionTexture.coordinatesMode, this.reflectionTexture.level, (this.reflectionTexture.isCube) ? 1 : 0);
            }
            if (this.emissiveTexture && BABYLON.StandardMaterial.EmissiveTextureEnabled) {
                this._effect.setTexture("emissiveSampler", this.emissiveTexture);
                this._effect.setFloat2("vEmissiveInfos", this.emissiveTexture.coordinatesIndex, this.emissiveTexture.level);
                this._effect.setMatrix("emissiveMatrix", this.emissiveTexture.getTextureMatrix());
            }
            if (this.specularTexture && BABYLON.StandardMaterial.SpecularTextureEnabled) {
                this._effect.setTexture("specularSampler", this.specularTexture);
                this._effect.setFloat2("vSpecularInfos", this.specularTexture.coordinatesIndex, this.specularTexture.level);
                this._effect.setMatrix("specularMatrix", this.specularTexture.getTextureMatrix());
            }
            if (this.bumpTexture && scene.getEngine().getCaps().standardDerivatives && BABYLON.StandardMaterial.BumpTextureEnabled) {
                this._effect.setTexture("bumpSampler", this.bumpTexture);
                this._effect.setFloat2("vBumpInfos", this.bumpTexture.coordinatesIndex, this.bumpTexture.level);
                this._effect.setMatrix("bumpMatrix", this.bumpTexture.getTextureMatrix());
            }
            scene.ambientColor.multiplyToRef(this.ambientColor, this._globalAmbientColor);
            this._effect.setVector3("vEyePosition", scene.activeCamera.position);
            this._effect.setColor3("vAmbientColor", this._globalAmbientColor);
            this._effect.setColor4("vDiffuseColor", this._baseColor, this.alpha * mesh.visibility);
            this._effect.setColor4("vSpecularColor", this.specularColor, this.specularPower);
            this._effect.setColor3("vEmissiveColor", this.emissiveColor);
            if (scene.lightsEnabled) {
                var lightIndex = 0;
                for (var index = 0; index < scene.lights.Length; index++) {
                    var light = scene.lights[index];
                    if (!light.isEnabled()) {
                        continue;
                    }
                    if (mesh && light.excludedMeshes.indexOf(mesh) != -1) {
                        continue;
                    }
                    if (light is BABYLON.PointLight) {
                        light.transferToEffect(this._effect, "vLightData" + lightIndex);
                    } else
                    if (light is BABYLON.DirectionalLight) {
                        light.transferToEffect(this._effect, "vLightData" + lightIndex);
                    } else
                    if (light is BABYLON.SpotLight) {
                        light.transferToEffect(this._effect, "vLightData" + lightIndex, "vLightDirection" + lightIndex);
                    } else
                    if (light is BABYLON.HemisphericLight) {
                        light.transferToEffect(this._effect, "vLightData" + lightIndex, "vLightGround" + lightIndex);
                    }
                    light.diffuse.scaleToRef(light.intensity, this._scaledDiffuse);
                    light.specular.scaleToRef(light.intensity, this._scaledSpecular);
                    this._effect.setColor4("vLightDiffuse" + lightIndex, this._scaledDiffuse, light.range);
                    this._effect.setColor3("vLightSpecular" + lightIndex, this._scaledSpecular);
                    var shadowGenerator = light.getShadowGenerator();
                    if (mesh.receiveShadows && shadowGenerator) {
                        this._effect.setMatrix("lightMatrix" + lightIndex, shadowGenerator.getTransformMatrix());
                        this._effect.setTexture("shadowSampler" + lightIndex, shadowGenerator.getShadowMap());
                        this._effect.setFloat("darkness" + lightIndex, shadowGenerator.getDarkness());
                    }
                    lightIndex++;
                    if (lightIndex == maxSimultaneousLights)
                        break;
                }
            }
            if (scene.clipPlane) {
                var clipPlane = scene.clipPlane;
                this._effect.setFloat4("vClipPlane", clipPlane.normal.x, clipPlane.normal.y, clipPlane.normal.z, clipPlane.d);
            }
            if (scene.fogMode != BABYLON.Scene.FOGMODE_NONE || this.reflectionTexture) {
                this._effect.setMatrix("view", scene.getViewMatrix());
            }
            if (scene.fogMode != BABYLON.Scene.FOGMODE_NONE) {
                this._effect.setFloat4("vFogInfos", scene.fogMode, scene.fogStart, scene.fogEnd, scene.fogDensity);
                this._effect.setColor3("vFogColor", scene.fogColor);
            }
        }
        public virtual Array < IAnimatable > getAnimatables() {
            var results = new Array < object > ();
            if (this.diffuseTexture && this.diffuseTexture.animations && this.diffuseTexture.animations.Length > 0) {
                results.push(this.diffuseTexture);
            }
            if (this.ambientTexture && this.ambientTexture.animations && this.ambientTexture.animations.Length > 0) {
                results.push(this.ambientTexture);
            }
            if (this.opacityTexture && this.opacityTexture.animations && this.opacityTexture.animations.Length > 0) {
                results.push(this.opacityTexture);
            }
            if (this.reflectionTexture && this.reflectionTexture.animations && this.reflectionTexture.animations.Length > 0) {
                results.push(this.reflectionTexture);
            }
            if (this.emissiveTexture && this.emissiveTexture.animations && this.emissiveTexture.animations.Length > 0) {
                results.push(this.emissiveTexture);
            }
            if (this.specularTexture && this.specularTexture.animations && this.specularTexture.animations.Length > 0) {
                results.push(this.specularTexture);
            }
            if (this.bumpTexture && this.bumpTexture.animations && this.bumpTexture.animations.Length > 0) {
                results.push(this.bumpTexture);
            }
            return results;
        }
        public virtual void dispose(bool forceDisposeEffect = false) {
            if (this.diffuseTexture) {
                this.diffuseTexture.dispose();
            }
            if (this.ambientTexture) {
                this.ambientTexture.dispose();
            }
            if (this.opacityTexture) {
                this.opacityTexture.dispose();
            }
            if (this.reflectionTexture) {
                this.reflectionTexture.dispose();
            }
            if (this.emissiveTexture) {
                this.emissiveTexture.dispose();
            }
            if (this.specularTexture) {
                this.specularTexture.dispose();
            }
            if (this.bumpTexture) {
                this.bumpTexture.dispose();
            }
            base.dispose(forceDisposeEffect);
        }
        public virtual StandardMaterial clone(string name) {
            var newStandardMaterial = new BABYLON.StandardMaterial(name, this.getScene());
            newStandardMaterial.checkReadyOnEveryCall = this.checkReadyOnEveryCall;
            newStandardMaterial.alpha = this.alpha;
            newStandardMaterial.wireframe = this.wireframe;
            newStandardMaterial.backFaceCulling = this.backFaceCulling;
            if (this.diffuseTexture && this.diffuseTexture.clone) {
                newStandardMaterial.diffuseTexture = this.diffuseTexture.clone();
            }
            if (this.ambientTexture && this.ambientTexture.clone) {
                newStandardMaterial.ambientTexture = this.ambientTexture.clone();
            }
            if (this.opacityTexture && this.opacityTexture.clone) {
                newStandardMaterial.opacityTexture = this.opacityTexture.clone();
            }
            if (this.reflectionTexture && this.reflectionTexture.clone) {
                newStandardMaterial.reflectionTexture = this.reflectionTexture.clone();
            }
            if (this.emissiveTexture && this.emissiveTexture.clone) {
                newStandardMaterial.emissiveTexture = this.emissiveTexture.clone();
            }
            if (this.specularTexture && this.specularTexture.clone) {
                newStandardMaterial.specularTexture = this.specularTexture.clone();
            }
            if (this.bumpTexture && this.bumpTexture.clone) {
                newStandardMaterial.bumpTexture = this.bumpTexture.clone();
            }
            newStandardMaterial.ambientColor = this.ambientColor.clone();
            newStandardMaterial.diffuseColor = this.diffuseColor.clone();
            newStandardMaterial.specularColor = this.specularColor.clone();
            newStandardMaterial.specularPower = this.specularPower;
            newStandardMaterial.emissiveColor = this.emissiveColor.clone();
            return newStandardMaterial;
        }
        public static bool DiffuseTextureEnabled = true;
        public static bool AmbientTextureEnabled = true;
        public static bool OpacityTextureEnabled = true;
        public static bool ReflectionTextureEnabled = true;
        public static bool EmissiveTextureEnabled = true;
        public static bool SpecularTextureEnabled = true;
        public static bool BumpTextureEnabled = true;
    }
}