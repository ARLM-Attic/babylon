using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web;
namespace BABYLON {
    public partial class Layer {
        public Texture texture;
        public bool isBackground;
        public Color4 color;
        public System.Action onDispose;
        private Scene _scene;
        private Array < object > _vertexDeclaration = new Array < object > (2);
        private double _vertexStrideSize = 2 * 4;
        private WebGLBuffer _vertexBuffer;
        private WebGLBuffer _indexBuffer;
        private Effect _effect;
        public string name;
        public Layer(string name, string imgUrl, Scene scene, bool isBackground = false, Color4 color = null) {
            this.texture = (imgUrl) ? new BABYLON.Texture(imgUrl, scene, true) : null;
            this.isBackground = (isBackground == null) ? true : isBackground;
            this.color = (color == null) ? new BABYLON.Color4(1, 1, 1, 1) : color;
            this._scene = scene;
            this._scene.layers.push(this);
            var vertices = new Array < object > ();
            vertices.push(1, 1);
            vertices.push(-1, 1);
            vertices.push(-1, -1);
            vertices.push(1, -1);
            this._vertexBuffer = scene.getEngine().createVertexBuffer(vertices);
            var indices = new Array < object > ();
            indices.push(0);
            indices.push(1);
            indices.push(2);
            indices.push(0);
            indices.push(2);
            indices.push(3);
            this._indexBuffer = scene.getEngine().createIndexBuffer(indices);
            this._effect = this._scene.getEngine().createEffect("layer", new Array < object > ("position"), new Array < object > ("textureMatrix", "color"), new Array < object > ("textureSampler"), "");
        }
        public virtual void render() {
            if (!this._effect.isReady() || !this.texture || !this.texture.isReady())
                return;
            var engine = this._scene.getEngine();
            engine.enableEffect(this._effect);
            engine.setState(false);
            this._effect.setTexture("textureSampler", this.texture);
            this._effect.setMatrix("textureMatrix", this.texture.getTextureMatrix());
            this._effect.setFloat4("color", this.color.r, this.color.g, this.color.b, this.color.a);
            engine.bindBuffers(this._vertexBuffer, this._indexBuffer, this._vertexDeclaration, this._vertexStrideSize, this._effect);
            engine.setAlphaMode(BABYLON.Engine.ALPHA_COMBINE);
            engine.draw(true, 0, 6);
            engine.setAlphaMode(BABYLON.Engine.ALPHA_DISABLE);
        }
        public virtual void dispose() {
            if (this._vertexBuffer) {
                this._scene.getEngine()._releaseBuffer(this._vertexBuffer);
                this._vertexBuffer = null;
            }
            if (this._indexBuffer) {
                this._scene.getEngine()._releaseBuffer(this._indexBuffer);
                this._indexBuffer = null;
            }
            if (this.texture) {
                this.texture.dispose();
                this.texture = null;
            }
            var index = this._scene.layers.indexOf(this);
            this._scene.layers.splice(index, 1);
            if (this.onDispose) {
                this.onDispose();
            }
        }
    }
}