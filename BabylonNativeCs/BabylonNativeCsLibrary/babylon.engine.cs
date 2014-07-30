using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web;
namespace BABYLON {
    public partial class EngineCapabilities {
        public double maxTexturesImageUnits;
        public double maxTextureSize;
        public double maxCubemapTextureSize;
        public double maxRenderTextureSize;
        public bool standardDerivatives;
        public dynamic s3tc;
        public bool textureFloat;
        public dynamic textureAnisotropicFilterExtension;
        public double maxAnisotropy;
        public dynamic instancedArrays;
    }
    public partial class Engine {
        private
        const double _ALPHA_DISABLE = 0;
        private
        const double _ALPHA_ADD = 1;
        private
        const double _ALPHA_COMBINE = 2;
        private
        const double _DELAYLOADSTATE_NONE = 0;
        private
        const double _DELAYLOADSTATE_LOADED = 1;
        private
        const double _DELAYLOADSTATE_LOADING = 2;
        private
        const double _DELAYLOADSTATE_NOTLOADED = 4;
        public static double ALPHA_DISABLE {
            get {
                return Engine._ALPHA_DISABLE;
            }
        }
        public static double ALPHA_ADD {
            get {
                return Engine._ALPHA_ADD;
            }
        }
        public static double ALPHA_COMBINE {
            get {
                return Engine._ALPHA_COMBINE;
            }
        }
        public static double DELAYLOADSTATE_NONE {
            get {
                return Engine._DELAYLOADSTATE_NONE;
            }
        }
        public static double DELAYLOADSTATE_LOADED {
            get {
                return Engine._DELAYLOADSTATE_LOADED;
            }
        }
        public static double DELAYLOADSTATE_LOADING {
            get {
                return Engine._DELAYLOADSTATE_LOADING;
            }
        }
        public static double DELAYLOADSTATE_NOTLOADED {
            get {
                return Engine._DELAYLOADSTATE_NOTLOADED;
            }
        }
        public static string Version {
            get {
                return "1.13.0";
            }
        }
        public static double Epsilon = 0.001;
        public static double CollisionsEpsilon = 0.001;
        public static string ShadersRepository = "Babylon/Shaders/";
        public bool isFullscreen = false;
        public bool isPointerLock = false;
        public bool forceWireframe = false;
        public bool cullBackFaces = true;
        public bool renderEvenInBackground = true;
        public Array < Scene > scenes = new Array < Scene > ();
        private WebGLRenderingContext _gl;
        private HTMLCanvasElement _renderingCanvas;
        private bool _windowIsBackground = false;
        private System.Action _onBlur;
        private System.Action _onFocus;
        private System.Action _onFullscreenChange;
        private System.Action _onPointerLockChange;
        private double _hardwareScalingLevel;
        private EngineCapabilities _caps;
        private bool _pointerLockRequested;
        private bool _alphaTest;
        private bool _runningLoop = false;
        private System.Action _renderFunction;
        private Array < WebGLTexture > _loadedTexturesCache = new Array < WebGLTexture > ();
        public Array < BaseTexture > _activeTexturesCache = new Array < BaseTexture > ();
        private Effect _currentEffect;
        private bool _cullingState;
        private dynamic _compiledEffects = new {};
        private Array < bool > _vertexAttribArrays;
        private bool _depthMask = false;
        private Viewport _cachedViewport;
        private object _cachedVertexBuffers;
        private WebGLBuffer _cachedIndexBuffer;
        private Effect _cachedEffectForVertexBuffers;
        private WebGLTexture _currentRenderTarget;
        private ClientRect _canvasClientRect;
        private HTMLCanvasElement _workingCanvas;
        private CanvasRenderingContext2D _workingContext;

        private Web.Window window;
        private Web.Document document;

        public Engine(HTMLCanvasElement canvas, bool antialias = false, object options = null) {
            this._renderingCanvas = canvas;
            this._canvasClientRect = this._renderingCanvas.getBoundingClientRect();
            options = options || new {};
            options.antialias = antialias;
            try {
                this._gl = canvas.getContext("webgl", options) || canvas.getContext("experimental-webgl", options);
            } catch (Exception e) {
                throw new Error("WebGL not supported");
            }
            if (!this._gl) {
                throw new Error("WebGL not supported");
            }
            this._onBlur = () => {
                this._windowIsBackground = true;
            };
            this._onFocus = () => {
                this._windowIsBackground = false;
            };
            window.addEventListener("blur", this._onBlur);
            window.addEventListener("focus", this._onFocus);
            this._workingCanvas = document.createElement("canvas");
            this._workingContext = this._workingCanvas.getContext("2d");
            this._hardwareScalingLevel = 1.0 / (window.devicePixelRatio || 1.0);
            this.resize();
            this._caps = new EngineCapabilities();
            this._caps.maxTexturesImageUnits = this._gl.getParameter(this._gl.MAX_TEXTURE_IMAGE_UNITS);
            this._caps.maxTextureSize = this._gl.getParameter(this._gl.MAX_TEXTURE_SIZE);
            this._caps.maxCubemapTextureSize = this._gl.getParameter(this._gl.MAX_CUBE_MAP_TEXTURE_SIZE);
            this._caps.maxRenderTextureSize = this._gl.getParameter(this._gl.MAX_RENDERBUFFER_SIZE);
            this._caps.standardDerivatives = (this._gl.getExtension("OES_standard_derivatives") != null);
            this._caps.s3tc = this._gl.getExtension("WEBGL_compressed_texture_s3tc");
            this._caps.textureFloat = (this._gl.getExtension("OES_texture_float") != null);
            this._caps.textureAnisotropicFilterExtension = this._gl.getExtension("EXT_texture_filter_anisotropic") || this._gl.getExtension("WEBKIT_EXT_texture_filter_anisotropic") || this._gl.getExtension("MOZ_EXT_texture_filter_anisotropic");
            this._caps.maxAnisotropy = (this._caps.textureAnisotropicFilterExtension) ? this._gl.getParameter(this._caps.textureAnisotropicFilterExtension.MAX_TEXTURE_MAX_ANISOTROPY_EXT) : 0;
            this._caps.instancedArrays = this._gl.getExtension("ANGLE_instanced_arrays");
            this.setDepthBuffer(true);
            this.setDepthFunctionToLessOrEqual();
            this.setDepthWrite(true);
            this._onFullscreenChange = () => {
                if (document.fullscreen != null) {
                    this.isFullscreen = document.fullscreen;
                } else
                if (document.mozFullScreen != null) {
                    this.isFullscreen = document.mozFullScreen;
                } else
                if (document.webkitIsFullScreen != null) {
                    this.isFullscreen = document.webkitIsFullScreen;
                } else
                if (document.msIsFullScreen != null) {
                    this.isFullscreen = document.msIsFullScreen;
                }
                if (this.isFullscreen && this._pointerLockRequested) {
                    canvas.requestPointerLock = canvas.requestPointerLock || canvas.msRequestPointerLock || canvas.mozRequestPointerLock || canvas.webkitRequestPointerLock;
                    if (canvas.requestPointerLock) {
                        canvas.requestPointerLock();
                    }
                }
            };
            document.addEventListener("fullscreenchange", this._onFullscreenChange, false);
            document.addEventListener("mozfullscreenchange", this._onFullscreenChange, false);
            document.addEventListener("webkitfullscreenchange", this._onFullscreenChange, false);
            document.addEventListener("msfullscreenchange", this._onFullscreenChange, false);
            this._onPointerLockChange = () => {
                this.isPointerLock = (document.mozPointerLockElement == canvas || document.webkitPointerLockElement == canvas || document.msPointerLockElement == canvas || document.pointerLockElement == canvas);
            };
            document.addEventListener("pointerlockchange", this._onPointerLockChange, false);
            document.addEventListener("mspointerlockchange", this._onPointerLockChange, false);
            document.addEventListener("mozpointerlockchange", this._onPointerLockChange, false);
            document.addEventListener("webkitpointerlockchange", this._onPointerLockChange, false);
        }
        void compileShader(WebGLRenderingContext gl, string source, string type, string defines) {
            var shader = gl.createShader((type == "vertex") ? gl.VERTEX_SHADER : gl.FRAGMENT_SHADER);
            gl.shaderSource(shader, ((defines) ? defines + "\\n" : "") + source);
            gl.compileShader(shader);
            if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
                throw new Error(gl.getShaderInfoLog(shader));
            }
            return shader;
        };
        void getSamplingParameters(double samplingMode, bool generateMipMaps, WebGLRenderingContext gl) {
            var magFilter = gl.NEAREST;
            var minFilter = gl.NEAREST;
            if (samplingMode == BABYLON.Texture.BILINEAR_SAMPLINGMODE) {
                magFilter = gl.LINEAR;
                if (generateMipMaps) {
                    minFilter = gl.LINEAR_MIPMAP_NEAREST;
                } else {
                    minFilter = gl.LINEAR;
                }
            } else
            if (samplingMode == BABYLON.Texture.TRILINEAR_SAMPLINGMODE) {
                magFilter = gl.LINEAR;
                if (generateMipMaps) {
                    minFilter = gl.LINEAR_MIPMAP_LINEAR;
                } else {
                    minFilter = gl.LINEAR;
                }
            } else
            if (samplingMode == BABYLON.Texture.NEAREST_SAMPLINGMODE) {
                magFilter = gl.NEAREST;
                if (generateMipMaps) {
                    minFilter = gl.NEAREST_MIPMAP_LINEAR;
                } else {
                    minFilter = gl.NEAREST;
                }
            }
            return new {};
        };
        void getExponantOfTwo(double value, double Max) {
            var count = 1;
            do {
                count *= 2;
            }
            while (count < value);
            if (count > Max)
                count = Max;
            return count;
        };
        void prepareWebGLTexture(WebGLTexture texture, WebGLRenderingContext gl, Scene scene, double width, double height, bool invertY, bool noMipmap, bool isCompressed, System.Action < double, double > processFunction, double samplingMode = Texture.TRILINEAR_SAMPLINGMODE) {
            var engine = scene.getEngine();
            var potWidth = getExponantOfTwo(width, engine.getCaps().maxTextureSize);
            var potHeight = getExponantOfTwo(height, engine.getCaps().maxTextureSize);
            gl.bindTexture(gl.TEXTURE_2D, texture);
            gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, (invertY == null) ? 1 : ((invertY) ? 1 : 0));
            processFunction(potWidth, potHeight);
            var filters = getSamplingParameters(samplingMode, !noMipmap, gl);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, filters.mag);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, filters.min);
            if (!noMipmap && !isCompressed) {
                gl.generateMipmap(gl.TEXTURE_2D);
            }
            gl.bindTexture(gl.TEXTURE_2D, null);
            engine._activeTexturesCache = new Array < object > ();
            texture._baseWidth = width;
            texture._baseHeight = height;
            texture._width = potWidth;
            texture._height = potHeight;
            texture.isReady = true;
            scene._removePendingData(texture);
        };
        void cascadeLoad(string rootUrl, double index, Array < HTMLImageElement > loadedImages, object scene, System.Action < Array < HTMLImageElement > > onfinish, Array < string > extensions) {
            var img;
            var onload = () => {
                loadedImages.push(img);
                scene._removePendingData(img);
                if (index != extensions.Length - 1) {
                    cascadeLoad(rootUrl, index + 1, loadedImages, scene, onfinish, extensions);
                } else {
                    onfinish(loadedImages);
                }
            };
            var onerror = () => {
                scene._removePendingData(img);
            };
            img = BABYLON.Tools.LoadImage(rootUrl + extensions[index], onload, onerror, scene.database);
            scene._addPendingData(img);
        };
        public virtual double getAspectRatio(Camera camera) {
            var viewport = camera.viewport;
            return (this.getRenderWidth() * viewport.width) / (this.getRenderHeight() * viewport.height);
        }
        public virtual double getRenderWidth() {
            if (this._currentRenderTarget) {
                return this._currentRenderTarget._width;
            }
            return this._renderingCanvas.width;
        }
        public virtual double getRenderHeight() {
            if (this._currentRenderTarget) {
                return this._currentRenderTarget._height;
            }
            return this._renderingCanvas.height;
        }
        public virtual HTMLCanvasElement getRenderingCanvas() {
            return this._renderingCanvas;
        }
        public virtual ClientRect getRenderingCanvasClientRect() {
            return this._renderingCanvas.getBoundingClientRect();
        }
        public virtual void setHardwareScalingLevel(double level) {
            this._hardwareScalingLevel = level;
            this.resize();
        }
        public virtual double getHardwareScalingLevel() {
            return this._hardwareScalingLevel;
        }
        public virtual Array < WebGLTexture > getLoadedTexturesCache() {
            return this._loadedTexturesCache;
        }
        public virtual EngineCapabilities getCaps() {
            return this._caps;
        }
        public virtual void setDepthFunctionToGreater() {
            this._gl.depthFunc(this._gl.GREATER);
        }
        public virtual void setDepthFunctionToGreaterOrEqual() {
            this._gl.depthFunc(this._gl.GEQUAL);
        }
        public virtual void setDepthFunctionToLess() {
            this._gl.depthFunc(this._gl.LESS);
        }
        public virtual void setDepthFunctionToLessOrEqual() {
            this._gl.depthFunc(this._gl.LEQUAL);
        }
        public virtual void stopRenderLoop() {
            this._renderFunction = null;
            this._runningLoop = false;
        }
        public virtual void _renderLoop() {
            var shouldRender = true;
            if (!this.renderEvenInBackground && this._windowIsBackground) {
                shouldRender = false;
            }
            if (shouldRender) {
                this.beginFrame();
                if (this._renderFunction) {
                    this._renderFunction();
                }
                this.endFrame();
            }
            if (this._runningLoop) {
                BABYLON.Tools.QueueNewFrame(() => {
                    this._renderLoop();
                });
            }
        }
        public virtual void runRenderLoop(System.Action renderFunction) {
            this._runningLoop = true;
            this._renderFunction = renderFunction;
            BABYLON.Tools.QueueNewFrame(() => {
                this._renderLoop();
            });
        }
        public virtual void switchFullscreen(bool requestPointerLock) {
            if (this.isFullscreen) {
                BABYLON.Tools.ExitFullscreen();
            } else {
                this._pointerLockRequested = requestPointerLock;
                BABYLON.Tools.RequestFullscreen(this._renderingCanvas);
            }
        }
        public virtual void clear(object color, bool backBuffer, bool depthStencil) {
            this._gl.clearColor(color.r, color.g, color.b, (color.a != null) ? color.a : 1.0);
            if (this._depthMask) {
                this._gl.clearDepth(1.0);
            }
            var mode = 0;
            if (backBuffer)
                mode |= this._gl.COLOR_BUFFER_BIT;
            if (depthStencil && this._depthMask)
                mode |= this._gl.DEPTH_BUFFER_BIT;
            this._gl.clear(mode);
        }
        public virtual void setViewport(Viewport viewport, double requiredWidth = 0.0, double requiredHeight = 0.0) {
            var width = requiredWidth || this._renderingCanvas.width;
            var height = requiredHeight || this._renderingCanvas.height;
            var x = viewport.x || 0;
            var y = viewport.y || 0;
            this._cachedViewport = viewport;
            this._gl.viewport(x * width, y * height, width * viewport.width, height * viewport.height);
        }
        public virtual void setDirectViewport(double x, double y, double width, double height) {
            this._cachedViewport = null;
            this._gl.viewport(x, y, width, height);
        }
        public virtual void beginFrame() {
            BABYLON.Tools._MeasureFps();
        }
        public virtual void endFrame() {
            this.flushFramebuffer();
        }
        public virtual void resize() {
            this._renderingCanvas.width = this._renderingCanvas.clientWidth / this._hardwareScalingLevel;
            this._renderingCanvas.height = this._renderingCanvas.clientHeight / this._hardwareScalingLevel;
            this._canvasClientRect = this._renderingCanvas.getBoundingClientRect();
        }
        public virtual void bindFramebuffer(WebGLTexture texture) {
            this._currentRenderTarget = texture;
            var gl = this._gl;
            gl.bindFramebuffer(gl.FRAMEBUFFER, texture._framebuffer);
            this._gl.viewport(0, 0, texture._width, texture._height);
            this.wipeCaches();
        }
        public virtual void unBindFramebuffer(WebGLTexture texture) {
            this._currentRenderTarget = null;
            if (texture.generateMipMaps) {
                var gl = this._gl;
                gl.bindTexture(gl.TEXTURE_2D, texture);
                gl.generateMipmap(gl.TEXTURE_2D);
                gl.bindTexture(gl.TEXTURE_2D, null);
            }
            this._gl.bindFramebuffer(this._gl.FRAMEBUFFER, null);
        }
        public virtual void flushFramebuffer() {
            this._gl.flush();
        }
        public virtual void restoreDefaultFramebuffer() {
            this._gl.bindFramebuffer(this._gl.FRAMEBUFFER, null);
            this.setViewport(this._cachedViewport);
            this.wipeCaches();
        }
        private void _resetVertexBufferBinding() {
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, null);
            this._cachedVertexBuffers = null;
        }
        public virtual WebGLBuffer createVertexBuffer(Array < double > vertices) {
            var vbo = this._gl.createBuffer();
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, vbo);
            this._gl.bufferData(this._gl.ARRAY_BUFFER, new Float32Array(vertices), this._gl.STATIC_DRAW);
            this._resetVertexBufferBinding();
            vbo.references = 1;
            return vbo;
        }
        public virtual WebGLBuffer createDynamicVertexBuffer(double capacity) {
            var vbo = this._gl.createBuffer();
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, vbo);
            this._gl.bufferData(this._gl.ARRAY_BUFFER, capacity, this._gl.DYNAMIC_DRAW);
            this._resetVertexBufferBinding();
            vbo.references = 1;
            return vbo;
        }
        public virtual void updateDynamicVertexBuffer(WebGLBuffer vertexBuffer, object vertices, double Length = 0.0) {
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, vertexBuffer);
            if (vertices is Float32Array) {
                this._gl.bufferSubData(this._gl.ARRAY_BUFFER, 0, vertices);
            } else {
                this._gl.bufferSubData(this._gl.ARRAY_BUFFER, 0, new Float32Array(vertices));
            }
            this._resetVertexBufferBinding();
        }
        private void _resetIndexBufferBinding() {
            this._gl.bindBuffer(this._gl.ELEMENT_ARRAY_BUFFER, null);
            this._cachedIndexBuffer = null;
        }
        public virtual WebGLBuffer createIndexBuffer(Array < double > indices) {
            var vbo = this._gl.createBuffer();
            this._gl.bindBuffer(this._gl.ELEMENT_ARRAY_BUFFER, vbo);
            this._gl.bufferData(this._gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indices), this._gl.STATIC_DRAW);
            this._resetIndexBufferBinding();
            vbo.references = 1;
            return vbo;
        }
        public virtual void bindBuffers(WebGLBuffer vertexBuffer, WebGLBuffer indexBuffer, Array < double > vertexDeclaration, double vertexStrideSize, Effect effect) {
            if (this._cachedVertexBuffers != vertexBuffer || this._cachedEffectForVertexBuffers != effect) {
                this._cachedVertexBuffers = vertexBuffer;
                this._cachedEffectForVertexBuffers = effect;
                this._gl.bindBuffer(this._gl.ARRAY_BUFFER, vertexBuffer);
                var offset = 0;
                for (var index = 0; index < vertexDeclaration.Length; index++) {
                    var order = effect.getAttributeLocation(index);
                    if (order >= 0) {
                        this._gl.vertexAttribPointer(order, vertexDeclaration[index], this._gl.FLOAT, false, vertexStrideSize, offset);
                    }
                    offset += vertexDeclaration[index] * 4;
                }
            }
            if (this._cachedIndexBuffer != indexBuffer) {
                this._cachedIndexBuffer = indexBuffer;
                this._gl.bindBuffer(this._gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
            }
        }
        public virtual void bindMultiBuffers(Array < VertexBuffer > vertexBuffers, WebGLBuffer indexBuffer, Effect effect) {
            if (this._cachedVertexBuffers != vertexBuffers || this._cachedEffectForVertexBuffers != effect) {
                this._cachedVertexBuffers = vertexBuffers;
                this._cachedEffectForVertexBuffers = effect;
                var attributes = effect.getAttributesNames();
                for (var index = 0; index < attributes.Length; index++) {
                    var order = effect.getAttributeLocation(index);
                    if (order >= 0) {
                        var vertexBuffer = vertexBuffers[attributes[index]];
                        if (!vertexBuffer) {
                            continue;
                        }
                        var stride = vertexBuffer.getStrideSize();
                        this._gl.bindBuffer(this._gl.ARRAY_BUFFER, vertexBuffer.getBuffer());
                        this._gl.vertexAttribPointer(order, stride, this._gl.FLOAT, false, stride * 4, 0);
                    }
                }
            }
            if (this._cachedIndexBuffer != indexBuffer) {
                this._cachedIndexBuffer = indexBuffer;
                this._gl.bindBuffer(this._gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
            }
        }
        public virtual bool _releaseBuffer(WebGLBuffer buffer) {
            buffer.references--;
            if (buffer.references == 0) {
                this._gl.deleteBuffer(buffer);
                return true;
            }
            return false;
        }
        public virtual WebGLBuffer createInstancesBuffer(double capacity) {
            var buffer = this._gl.createBuffer();
            buffer.capacity = capacity;
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, buffer);
            this._gl.bufferData(this._gl.ARRAY_BUFFER, capacity, this._gl.DYNAMIC_DRAW);
            return buffer;
        }
        public virtual void deleteInstancesBuffer(WebGLBuffer buffer) {
            this._gl.deleteBuffer(buffer);
        }
        public virtual void updateAndBindInstancesBuffer(WebGLBuffer instancesBuffer, Float32Array data, Array < double > offsetLocations) {
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, instancesBuffer);
            this._gl.bufferSubData(this._gl.ARRAY_BUFFER, 0, data);
            for (var index = 0; index < 4; index++) {
                var offsetLocation = offsetLocations[index];
                this._gl.enableVertexAttribArray(offsetLocation);
                this._gl.vertexAttribPointer(offsetLocation, 4, this._gl.FLOAT, false, 64, index * 16);
                this._caps.instancedArrays.vertexAttribDivisorANGLE(offsetLocation, 1);
            }
        }
        public virtual void unBindInstancesBuffer(WebGLBuffer instancesBuffer, Array < double > offsetLocations) {
            this._gl.bindBuffer(this._gl.ARRAY_BUFFER, instancesBuffer);
            for (var index = 0; index < 4; index++) {
                var offsetLocation = offsetLocations[index];
                this._gl.disableVertexAttribArray(offsetLocation);
                this._caps.instancedArrays.vertexAttribDivisorANGLE(offsetLocation, 0);
            }
        }
        public virtual void draw(bool useTriangles, double indexStart, double indexCount, double instancesCount = 0.0) {
            if (instancesCount) {
                this._caps.instancedArrays.drawElementsInstancedANGLE((useTriangles) ? this._gl.TRIANGLES : this._gl.LINES, indexCount, this._gl.UNSIGNED_SHORT, indexStart * 2, instancesCount);
                return;
            }
            this._gl.drawElements((useTriangles) ? this._gl.TRIANGLES : this._gl.LINES, indexCount, this._gl.UNSIGNED_SHORT, indexStart * 2);
        }
        public virtual void _releaseEffect(Effect effect) {
            if (this._compiledEffects[effect._key]) {
                this._compiledEffects[effect._key] = null;
                if (effect.getProgram()) {
                    this._gl.deleteProgram(effect.getProgram());
                }
            }
        }
        public virtual Effect createEffect(object baseName, Array < string > attributesNames, Array < string > uniformsNames, Array < string > samplers, string defines, Array < string > optionalDefines = null, System.Action < Effect > onCompiled = null, System.Action < Effect, string > onError = null) {
            var vertex = baseName.vertexElement || baseName.vertex || baseName;
            var fragment = baseName.fragmentElement || baseName.fragment || baseName;
            var name = vertex + "+" + fragment + "@" + defines;
            if (this._compiledEffects[name]) {
                return this._compiledEffects[name];
            }
            var effect = new BABYLON.Effect(baseName, attributesNames, uniformsNames, samplers, this, defines, optionalDefines, onCompiled, onError);
            effect._key = name;
            this._compiledEffects[name] = effect;
            return effect;
        }
        public virtual WebGLProgram createShaderProgram(string vertexCode, string fragmentCode, string defines) {
            var vertexShader = compileShader(this._gl, vertexCode, "vertex", defines);
            var fragmentShader = compileShader(this._gl, fragmentCode, "fragment", defines);
            var shaderProgram = this._gl.createProgram();
            this._gl.attachShader(shaderProgram, vertexShader);
            this._gl.attachShader(shaderProgram, fragmentShader);
            this._gl.linkProgram(shaderProgram);
            var linked = this._gl.getProgramParameter(shaderProgram, this._gl.LINK_STATUS);
            if (!linked) {
                var error = this._gl.getProgramInfoLog(shaderProgram);
                if (error) {
                    throw new Error(error);
                }
            }
            this._gl.deleteShader(vertexShader);
            this._gl.deleteShader(fragmentShader);
            return shaderProgram;
        }
        public virtual Array < WebGLUniformLocation > getUniforms(WebGLProgram shaderProgram, Array < string > uniformsNames) {
            var results = new Array < object > ();
            for (var index = 0; index < uniformsNames.Length; index++) {
                results.push(this._gl.getUniformLocation(shaderProgram, uniformsNames[index]));
            }
            return results;
        }
        public virtual Array < double > getAttributes(WebGLProgram shaderProgram, Array < string > attributesNames) {
            var results = new Array < object > ();
            for (var index = 0; index < attributesNames.Length; index++) {
                try {
                    results.push(this._gl.getAttribLocation(shaderProgram, attributesNames[index]));
                } catch (Exception e) {
                    results.push(-1);
                }
            }
            return results;
        }
        public virtual void enableEffect(Effect effect) {
            if (!effect || !effect.getAttributesCount() || this._currentEffect == effect) {
                return;
            }
            this._vertexAttribArrays = this._vertexAttribArrays || new Array < object > ();
            this._gl.useProgram(effect.getProgram());
            foreach(var i in this._vertexAttribArrays) {
                if (i > this._gl.VERTEX_ATTRIB_ARRAY_ENABLED || !this._vertexAttribArrays[i]) {
                    continue;
                }
                this._vertexAttribArrays[i] = false;
                this._gl.disableVertexAttribArray(i);
            }
            var attributesCount = effect.getAttributesCount();
            for (var index = 0; index < attributesCount; index++) {
                var order = effect.getAttributeLocation(index);
                if (order >= 0) {
                    this._vertexAttribArrays[order] = true;
                    this._gl.enableVertexAttribArray(order);
                }
            }
            this._currentEffect = effect;
        }
        public virtual void setArray(WebGLUniformLocation uniform, Array < double > array) {
            if (!uniform)
                return;
            this._gl.uniform1fv(uniform, array);
        }
        public virtual void setMatrices(WebGLUniformLocation uniform, Float32Array matrices) {
            if (!uniform)
                return;
            this._gl.uniformMatrix4fv(uniform, false, matrices);
        }
        public virtual void setMatrix(WebGLUniformLocation uniform, Matrix matrix) {
            if (!uniform)
                return;
            this._gl.uniformMatrix4fv(uniform, false, matrix.toArray());
        }
        public virtual void setFloat(WebGLUniformLocation uniform, double value) {
            if (!uniform)
                return;
            this._gl.uniform1f(uniform, value);
        }
        public virtual void setFloat2(WebGLUniformLocation uniform, double x, double y) {
            if (!uniform)
                return;
            this._gl.uniform2f(uniform, x, y);
        }
        public virtual void setFloat3(WebGLUniformLocation uniform, double x, double y, double z) {
            if (!uniform)
                return;
            this._gl.uniform3f(uniform, x, y, z);
        }
        public virtual void setBool(WebGLUniformLocation uniform, double _bool) {
            if (!uniform)
                return;
            this._gl.uniform1i(uniform, _bool);
        }
        public virtual void setFloat4(WebGLUniformLocation uniform, double x, double y, double z, double w) {
            if (!uniform)
                return;
            this._gl.uniform4f(uniform, x, y, z, w);
        }
        public virtual void setColor3(WebGLUniformLocation uniform, Color3 color3) {
            if (!uniform)
                return;
            this._gl.uniform3f(uniform, color3.r, color3.g, color3.b);
        }
        public virtual void setColor4(WebGLUniformLocation uniform, Color3 color3, double alpha) {
            if (!uniform)
                return;
            this._gl.uniform4f(uniform, color3.r, color3.g, color3.b, alpha);
        }
        public virtual void setState(bool culling) {
            if (this._cullingState != culling) {
                if (culling) {
                    this._gl.cullFace((this.cullBackFaces) ? this._gl.BACK : this._gl.FRONT);
                    this._gl.enable(this._gl.CULL_FACE);
                } else {
                    this._gl.disable(this._gl.CULL_FACE);
                }
                this._cullingState = culling;
            }
        }
        public virtual void setDepthBuffer(bool enable) {
            if (enable) {
                this._gl.enable(this._gl.DEPTH_TEST);
            } else {
                this._gl.disable(this._gl.DEPTH_TEST);
            }
        }
        public virtual void setDepthWrite(bool enable) {
            this._gl.depthMask(enable);
            this._depthMask = enable;
        }
        public virtual void setColorWrite(bool enable) {
            this._gl.colorMask(enable, enable, enable, enable);
        }
        public virtual void setAlphaMode(double mode) {
            switch (mode) {
                case BABYLON.Engine.ALPHA_DISABLE:
                    this.setDepthWrite(true);
                    this._gl.disable(this._gl.BLEND);
                    break;
                case BABYLON.Engine.ALPHA_COMBINE:
                    this.setDepthWrite(false);
                    this._gl.blendFuncSeparate(this._gl.SRC_ALPHA, this._gl.ONE_MINUS_SRC_ALPHA, this._gl.ONE, this._gl.ONE);
                    this._gl.enable(this._gl.BLEND);
                    break;
                case BABYLON.Engine.ALPHA_ADD:
                    this.setDepthWrite(false);
                    this._gl.blendFuncSeparate(this._gl.ONE, this._gl.ONE, this._gl.ZERO, this._gl.ONE);
                    this._gl.enable(this._gl.BLEND);
                    break;
            }
        }
        public virtual void setAlphaTesting(bool enable) {
            this._alphaTest = enable;
        }
        public virtual bool getAlphaTesting() {
            return this._alphaTest;
        }
        public virtual void wipeCaches() {
            this._activeTexturesCache = new Array < object > ();
            this._currentEffect = null;
            this._cullingState = null;
            this._cachedVertexBuffers = null;
            this._cachedIndexBuffer = null;
            this._cachedEffectForVertexBuffers = null;
        }
        public virtual void setSamplingMode(WebGLTexture texture, double samplingMode) {
            var gl = this._gl;
            gl.bindTexture(gl.TEXTURE_2D, texture);
            var magFilter = gl.NEAREST;
            var minFilter = gl.NEAREST;
            if (samplingMode == BABYLON.Texture.BILINEAR_SAMPLINGMODE) {
                magFilter = gl.LINEAR;
                minFilter = gl.LINEAR;
            } else
            if (samplingMode == BABYLON.Texture.TRILINEAR_SAMPLINGMODE) {
                magFilter = gl.LINEAR;
                minFilter = gl.LINEAR_MIPMAP_LINEAR;
            }
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, magFilter);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, minFilter);
            gl.bindTexture(gl.TEXTURE_2D, null);
        }
        public virtual WebGLTexture createTexture(string url, bool noMipmap, bool invertY, Scene scene, double samplingMode = Texture.TRILINEAR_SAMPLINGMODE) {
            var texture = this._gl.createTexture();
            var extension = url.substr(url.Length - 4, 4).toLowerCase();
            var isDDS = this.getCaps().s3tc && (extension == ".dds");
            var isTGA = (extension == ".tga");
            scene._addPendingData(texture);
            texture.url = url;
            texture.noMipmap = noMipmap;
            texture.references = 1;
            this._loadedTexturesCache.push(texture);
            if (isTGA) {
                BABYLON.Tools.LoadFile(url, (arrayBuffer) => {
                    var data = new Uint8Array(arrayBuffer);
                    var header = BABYLON.Internals.TGATools.GetTGAHeader(data);
                    prepareWebGLTexture(texture, this._gl, scene, header.width, header.height, invertY, noMipmap, false, () => {
                        Internals.TGATools.UploadContent(this._gl, data);
                    }, samplingMode);
                }, null, scene.database, true);
            } else
            if (isDDS) {
                BABYLON.Tools.LoadFile(url, (data) => {
                    var info = BABYLON.Internals.DDSTools.GetDDSInfo(data);
                    var loadMipmap = (info.isRGB || info.isLuminance || info.mipmapCount > 1) && !noMipmap && ((info.width << (info.mipmapCount - 1)) == 1);
                    prepareWebGLTexture(texture, this._gl, scene, info.width, info.height, invertY, !loadMipmap, info.isFourCC, () => {
                        console.log("loading " + url);
                        Internals.DDSTools.UploadDDSLevels(this._gl, this.getCaps().s3tc, data, info, loadMipmap, 1);
                    }, samplingMode);
                }, null, scene.database, true);
            } else {
                var onload = (object img) => {
                    prepareWebGLTexture(texture, this._gl, scene, img.width, img.height, invertY, noMipmap, false, (object potWidth, object potHeight) => {
                        var isPot = (img.width == potWidth && img.height == potHeight);
                        if (!isPot) {
                            this._workingCanvas.width = potWidth;
                            this._workingCanvas.height = potHeight;
                            this._workingContext.drawImage(img, 0, 0, img.width, img.height, 0, 0, potWidth, potHeight);
                        }
                        this._gl.texImage2D(this._gl.TEXTURE_2D, 0, this._gl.RGBA, this._gl.RGBA, this._gl.UNSIGNED_BYTE, (isPot) ? img : this._workingCanvas);
                    }, samplingMode);
                };
                var onerror = () => {
                    scene._removePendingData(texture);
                };
                BABYLON.Tools.LoadImage(url, onload, onerror, scene.database);
            }
            return texture;
        }
        public virtual WebGLTexture createDynamicTexture(double width, double height, bool generateMipMaps, double samplingMode) {
            var texture = this._gl.createTexture();
            width = getExponantOfTwo(width, this._caps.maxTextureSize);
            height = getExponantOfTwo(height, this._caps.maxTextureSize);
            this._gl.bindTexture(this._gl.TEXTURE_2D, texture);
            var filters = getSamplingParameters(samplingMode, generateMipMaps, this._gl);
            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_MAG_FILTER, filters.mag);
            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_MIN_FILTER, filters.min);
            this._gl.bindTexture(this._gl.TEXTURE_2D, null);
            this._activeTexturesCache = new Array < object > ();
            texture._baseWidth = width;
            texture._baseHeight = height;
            texture._width = width;
            texture._height = height;
            texture.isReady = false;
            texture.generateMipMaps = generateMipMaps;
            texture.references = 1;
            this._loadedTexturesCache.push(texture);
            return texture;
        }
        public virtual void updateDynamicTexture(WebGLTexture texture, HTMLCanvasElement canvas, bool invertY) {
            this._gl.bindTexture(this._gl.TEXTURE_2D, texture);
            this._gl.pixelStorei(this._gl.UNPACK_FLIP_Y_WEBGL, (invertY) ? 1 : 0);
            this._gl.texImage2D(this._gl.TEXTURE_2D, 0, this._gl.RGBA, this._gl.RGBA, this._gl.UNSIGNED_BYTE, canvas);
            if (texture.generateMipMaps) {
                this._gl.generateMipmap(this._gl.TEXTURE_2D);
            }
            this._gl.bindTexture(this._gl.TEXTURE_2D, null);
            this._activeTexturesCache = new Array < object > ();
            texture.isReady = true;
        }
        public virtual void updateVideoTexture(WebGLTexture texture, HTMLVideoElement video, bool invertY) {
            this._gl.bindTexture(this._gl.TEXTURE_2D, texture);
            this._gl.pixelStorei(this._gl.UNPACK_FLIP_Y_WEBGL, (invertY) ? 0 : 1);
            if (video.videoWidth != texture._width || video.videoHeight != texture._height) {
                if (!texture._workingCanvas) {
                    texture._workingCanvas = document.createElement("canvas");
                    texture._workingContext = texture._workingCanvas.getContext("2d");
                    texture._workingCanvas.width = texture._width;
                    texture._workingCanvas.height = texture._height;
                }
                texture._workingContext.drawImage(video, 0, 0, video.videoWidth, video.videoHeight, 0, 0, texture._width, texture._height);
                this._gl.texImage2D(this._gl.TEXTURE_2D, 0, this._gl.RGBA, this._gl.RGBA, this._gl.UNSIGNED_BYTE, texture._workingCanvas);
            } else {
                this._gl.texImage2D(this._gl.TEXTURE_2D, 0, this._gl.RGBA, this._gl.RGBA, this._gl.UNSIGNED_BYTE, video);
            }
            if (texture.generateMipMaps) {
                this._gl.generateMipmap(this._gl.TEXTURE_2D);
            }
            this._gl.bindTexture(this._gl.TEXTURE_2D, null);
            this._activeTexturesCache = new Array < object > ();
            texture.isReady = true;
        }
        public virtual WebGLTexture createRenderTargetTexture(object size, object options) {
            var generateMipMaps = false;
            var generateDepthBuffer = true;
            var samplingMode = BABYLON.Texture.TRILINEAR_SAMPLINGMODE;
            if (options != null) {
                generateMipMaps = (options.generateMipMaps == null) ? options : options.generateMipmaps;
                generateDepthBuffer = (options.generateDepthBuffer == null) ? true : options.generateDepthBuffer;
                if (options.samplingMode != null) {
                    samplingMode = options.samplingMode;
                }
            }
            var gl = this._gl;
            var texture = gl.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, texture);
            var width = size.width || size;
            var height = size.height || size;
            var filters = getSamplingParameters(samplingMode, generateMipMaps, gl);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, filters.mag);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, filters.min);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, width, height, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);
            var depthBuffer;
            if (generateDepthBuffer) {
                depthBuffer = gl.createRenderbuffer();
                gl.bindRenderbuffer(gl.RENDERBUFFER, depthBuffer);
                gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, width, height);
            }
            var framebuffer = gl.createFramebuffer();
            gl.bindFramebuffer(gl.FRAMEBUFFER, framebuffer);
            gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, texture, 0);
            if (generateDepthBuffer) {
                gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, depthBuffer);
            }
            gl.bindTexture(gl.TEXTURE_2D, null);
            gl.bindRenderbuffer(gl.RENDERBUFFER, null);
            gl.bindFramebuffer(gl.FRAMEBUFFER, null);
            texture._framebuffer = framebuffer;
            if (generateDepthBuffer) {
                texture._depthBuffer = depthBuffer;
            }
            texture._width = width;
            texture._height = height;
            texture.isReady = true;
            texture.generateMipMaps = generateMipMaps;
            texture.references = 1;
            this._activeTexturesCache = new Array < object > ();
            this._loadedTexturesCache.push(texture);
            return texture;
        }
        public virtual WebGLTexture createCubeTexture(string rootUrl, Scene scene, Array < string > extensions, bool noMipmap = false) {
            var gl = this._gl;
            var texture = gl.createTexture();
            texture.isCube = true;
            texture.url = rootUrl;
            texture.references = 1;
            this._loadedTexturesCache.push(texture);
            var extension = rootUrl.substr(rootUrl.Length - 4, 4).toLowerCase();
            var isDDS = this.getCaps().s3tc && (extension == ".dds");
            if (isDDS) {
                BABYLON.Tools.LoadFile(rootUrl, (data) => {
                    var info = BABYLON.Internals.DDSTools.GetDDSInfo(data);
                    var loadMipmap = (info.isRGB || info.isLuminance || info.mipmapCount > 1) && !noMipmap;
                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, texture);
                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                    Internals.DDSTools.UploadDDSLevels(this._gl, this.getCaps().s3tc, data, info, loadMipmap, 6);
                    if (!noMipmap && !info.isFourCC && info.mipmapCount == 1) {
                        gl.generateMipmap(gl.TEXTURE_CUBE_MAP);
                    }
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MIN_FILTER, (loadMipmap) ? gl.LINEAR_MIPMAP_LINEAR : gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, null);
                    this._activeTexturesCache = new Array < object > ();
                    texture._width = info.width;
                    texture._height = info.height;
                    texture.isReady = true;
                });
            } else {
                cascadeLoad(rootUrl, 0, new Array < object > (), scene, (imgs) => {
                    var width = getExponantOfTwo(imgs[0].width, this._caps.maxCubemapTextureSize);
                    var height = width;
                    this._workingCanvas.width = width;
                    this._workingCanvas.height = height;
                    var faces = new Array < object > (gl.TEXTURE_CUBE_MAP_POSITIVE_X, gl.TEXTURE_CUBE_MAP_POSITIVE_Y, gl.TEXTURE_CUBE_MAP_POSITIVE_Z, gl.TEXTURE_CUBE_MAP_NEGATIVE_X, gl.TEXTURE_CUBE_MAP_NEGATIVE_Y, gl.TEXTURE_CUBE_MAP_NEGATIVE_Z);
                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, texture);
                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 0);
                    for (var index = 0; index < faces.Length; index++) {
                        this._workingContext.drawImage(imgs[index], 0, 0, imgs[index].width, imgs[index].height, 0, 0, width, height);
                        gl.texImage2D(faces[index], 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, this._workingCanvas);
                    }
                    if (!noMipmap) {
                        gl.generateMipmap(gl.TEXTURE_CUBE_MAP);
                    }
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_MIN_FILTER, (noMipmap) ? gl.LINEAR : gl.LINEAR_MIPMAP_LINEAR);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
                    gl.texParameteri(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
                    gl.bindTexture(gl.TEXTURE_CUBE_MAP, null);
                    this._activeTexturesCache = new Array < object > ();
                    texture._width = width;
                    texture._height = height;
                    texture.isReady = true;
                }, extensions);
            }
            return texture;
        }
        public virtual void _releaseTexture(WebGLTexture texture) {
            var gl = this._gl;
            if (texture._framebuffer) {
                gl.deleteFramebuffer(texture._framebuffer);
            }
            if (texture._depthBuffer) {
                gl.deleteRenderbuffer(texture._depthBuffer);
            }
            gl.deleteTexture(texture);
            for (var channel = 0; channel < this._caps.maxTexturesImageUnits; channel++) {
                this._gl.activeTexture(this._gl["TEXTURE" + channel]);
                this._gl.bindTexture(this._gl.TEXTURE_2D, null);
                this._gl.bindTexture(this._gl.TEXTURE_CUBE_MAP, null);
                this._activeTexturesCache[channel] = null;
            }
            var index = this._loadedTexturesCache.indexOf(texture);
            if (index != -1) {
                this._loadedTexturesCache.splice(index, 1);
            }
        }
        public virtual void bindSamplers(Effect effect) {
            this._gl.useProgram(effect.getProgram());
            var samplers = effect.getSamplers();
            for (var index = 0; index < samplers.Length; index++) {
                var uniform = effect.getUniform(samplers[index]);
                this._gl.uniform1i(uniform, index);
            }
            this._currentEffect = null;
        }
        public virtual void _bindTexture(double channel, WebGLTexture texture) {
            this._gl.activeTexture(this._gl["TEXTURE" + channel]);
            this._gl.bindTexture(this._gl.TEXTURE_2D, texture);
            this._activeTexturesCache[channel] = null;
        }
        public virtual void setTextureFromPostProcess(double channel, PostProcess postProcess) {
            this._bindTexture(channel, postProcess._textures.data[postProcess._currentRenderTextureInd]);
        }
        public virtual void setTexture(double channel, BaseTexture texture) {
            if (channel < 0) {
                return;
            }
            if (!texture || !texture.isReady()) {
                if (this._activeTexturesCache[channel] != null) {
                    this._gl.activeTexture(this._gl["TEXTURE" + channel]);
                    this._gl.bindTexture(this._gl.TEXTURE_2D, null);
                    this._gl.bindTexture(this._gl.TEXTURE_CUBE_MAP, null);
                    this._activeTexturesCache[channel] = null;
                }
                return;
            }
            if (texture is BABYLON.VideoTexture) {
                if (((VideoTexture) texture).update()) {
                    this._activeTexturesCache[channel] = null;
                }
            } else
            if (texture.delayLoadState == BABYLON.Engine.DELAYLOADSTATE_NOTLOADED) {
                texture.delayLoad();
                return;
            }
            if (this._activeTexturesCache[channel] == texture) {
                return;
            }
            this._activeTexturesCache[channel] = texture;
            var internalTexture = texture.getInternalTexture();
            this._gl.activeTexture(this._gl["TEXTURE" + channel]);
            if (internalTexture.isCube) {
                this._gl.bindTexture(this._gl.TEXTURE_CUBE_MAP, internalTexture);
                if (internalTexture._cachedCoordinatesMode != texture.coordinatesMode) {
                    internalTexture._cachedCoordinatesMode = texture.coordinatesMode;
                    var textureWrapMode = ((texture.coordinatesMode != BABYLON.Texture.CUBIC_MODE && texture.coordinatesMode != BABYLON.Texture.SKYBOX_MODE)) ? this._gl.REPEAT : this._gl.CLAMP_TO_EDGE;
                    this._gl.texParameteri(this._gl.TEXTURE_CUBE_MAP, this._gl.TEXTURE_WRAP_S, textureWrapMode);
                    this._gl.texParameteri(this._gl.TEXTURE_CUBE_MAP, this._gl.TEXTURE_WRAP_T, textureWrapMode);
                }
                this._setAnisotropicLevel(this._gl.TEXTURE_CUBE_MAP, texture);
            } else {
                this._gl.bindTexture(this._gl.TEXTURE_2D, internalTexture);
                if (internalTexture._cachedWrapU != texture.wrapU) {
                    internalTexture._cachedWrapU = texture.wrapU;
                    switch (texture.wrapU) {
                        case BABYLON.Texture.WRAP_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_S, this._gl.REPEAT);
                            break;
                        case BABYLON.Texture.CLAMP_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_S, this._gl.CLAMP_TO_EDGE);
                            break;
                        case BABYLON.Texture.MIRROR_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_S, this._gl.MIRRORED_REPEAT);
                            break;
                    }
                }
                if (internalTexture._cachedWrapV != texture.wrapV) {
                    internalTexture._cachedWrapV = texture.wrapV;
                    switch (texture.wrapV) {
                        case BABYLON.Texture.WRAP_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_T, this._gl.REPEAT);
                            break;
                        case BABYLON.Texture.CLAMP_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_T, this._gl.CLAMP_TO_EDGE);
                            break;
                        case BABYLON.Texture.MIRROR_ADDRESSMODE:
                            this._gl.texParameteri(this._gl.TEXTURE_2D, this._gl.TEXTURE_WRAP_T, this._gl.MIRRORED_REPEAT);
                            break;
                    }
                }
                this._setAnisotropicLevel(this._gl.TEXTURE_2D, texture);
            }
        }
        public virtual void _setAnisotropicLevel(double key, BaseTexture texture) {
            var anisotropicFilterExtension = this._caps.textureAnisotropicFilterExtension;
            if (anisotropicFilterExtension && texture._cachedAnisotropicFilteringLevel != texture.anisotropicFilteringLevel) {
                this._gl.texParameterf(key, anisotropicFilterExtension.TEXTURE_MAX_ANISOTROPY_EXT, Math.min(texture.anisotropicFilteringLevel, this._caps.maxAnisotropy));
                texture._cachedAnisotropicFilteringLevel = texture.anisotropicFilteringLevel;
            }
        }
        public virtual Uint8Array readPixels(double x, double y, double width, double height) {
            var data = new Uint8Array(height * width * 4);
            this._gl.readPixels(0, 0, width, height, this._gl.RGBA, this._gl.UNSIGNED_BYTE, data);
            return data;
        }
        public virtual void dispose() {
            this.stopRenderLoop();
            while (this.scenes.Length) {
                this.scenes[0].dispose();
            }
            foreach(var name in this._compiledEffects) {
                this._gl.deleteProgram(this._compiledEffects[name]._program);
            }
            foreach(var i in this._vertexAttribArrays) {
                if (i > this._gl.VERTEX_ATTRIB_ARRAY_ENABLED || !this._vertexAttribArrays[i]) {
                    continue;
                }
                this._gl.disableVertexAttribArray(i);
            }
            window.removeEventListener("blur", this._onBlur);
            window.removeEventListener("focus", this._onFocus);
            document.removeEventListener("fullscreenchange", this._onFullscreenChange);
            document.removeEventListener("mozfullscreenchange", this._onFullscreenChange);
            document.removeEventListener("webkitfullscreenchange", this._onFullscreenChange);
            document.removeEventListener("msfullscreenchange", this._onFullscreenChange);
            document.removeEventListener("pointerlockchange", this._onPointerLockChange);
            document.removeEventListener("mspointerlockchange", this._onPointerLockChange);
            document.removeEventListener("mozpointerlockchange", this._onPointerLockChange);
            document.removeEventListener("webkitpointerlockchange", this._onPointerLockChange);
        }
        public static bool isSupported() {
            try {
                var tempcanvas = document.createElement("canvas");
                var gl = tempcanvas.getContext("webgl") || tempcanvas.getContext("experimental-webgl");
                return gl != null && !!window.WebGLRenderingContext;
            } catch (Exception e) {
                return false;
            }
        }
    }
}