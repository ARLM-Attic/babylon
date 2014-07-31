using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web;
namespace BABYLON {
    public partial class InterpolateValueAction: Action {
        private object _target;
        private string _property;
        public string propertyPath;
        public object value;
        public double duration;
        public bool stopOtherAnimations;
        public InterpolateValueAction(object triggerOptions, object target, string propertyPath, object value, double duration = 1000, Condition condition = null, bool stopOtherAnimations = false): base(triggerOptions, condition) {
            this._target = target;
        }
        public virtual void _prepare() {
            this._target = this._getEffectiveTarget(this._target, this.propertyPath);
            this._property = this._getProperty(this.propertyPath);
        }
        public virtual void execute() {
            var scene = this._actionManager.getScene();
            var keys = new Array < object > (new {}, new {});
            var dataType;
            if (typeof(this.value) == "number") {
                dataType = Animation.ANIMATIONTYPE_FLOAT;
            } else
            if (this.value is Color3) {
                dataType = Animation.ANIMATIONTYPE_COLOR3;
            } else
            if (this.value is Vector3) {
                dataType = Animation.ANIMATIONTYPE_VECTOR3;
            } else
            if (this.value is Matrix) {
                dataType = Animation.ANIMATIONTYPE_MATRIX;
            } else
            if (this.value is Quaternion) {
                dataType = Animation.ANIMATIONTYPE_QUATERNION;
            } else {
                Tools.Warn("InterpolateValueAction: Unsupported type (" + typeof(this.value) + ")");
                return;
            }
            var animation = new BABYLON.Animation("InterpolateValueAction", this._property, 100 * (1000.0 / this.duration), dataType, Animation.ANIMATIONLOOPMODE_CONSTANT);
            animation.setKeys(keys);
            if (this.stopOtherAnimations) {
                scene.stopAnimation(this._target);
            }
            scene.beginDirectAnimation(this._target, new Array < object > (animation), 0, 100);
        }
    }
}