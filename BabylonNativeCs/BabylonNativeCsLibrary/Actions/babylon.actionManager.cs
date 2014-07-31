using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web;
namespace BABYLON
{
    public partial class ActionEvent
    {
        public AbstractMesh source;
        public double pointerX;
        public double pointerY;
        public AbstractMesh meshUnderPointer;
        public ActionEvent(AbstractMesh source, double pointerX, double pointerY, AbstractMesh meshUnderPointer) { }
        public static ActionEvent CreateNew(AbstractMesh source)
        {
            var scene = source.getScene();
            return new ActionEvent(source, scene.pointerX, scene.pointerY, scene.meshUnderPointer);
        }
    }
    public partial class ActionManager
    {
        private static int _NothingTrigger = 0;
        private static int _OnPickTrigger = 1;
        private static int _OnLeftPickTrigger = 2;
        private static int _OnRightPickTrigger = 3;
        private static int _OnCenterPickTrigger = 4;
        private static int _OnPointerOverTrigger = 5;
        private static int _OnPointerOutTrigger = 6;
        private static int _OnEveryFrameTrigger = 7;
        private static int _OnIntersectionEnterTrigger = 8;
        private static int _OnIntersectionExitTrigger = 9;
        public static int NothingTrigger
        {
            get
            {
                return ActionManager._NothingTrigger;
            }
        }
        public static int OnPickTrigger
        {
            get
            {
                return ActionManager._OnPickTrigger;
            }
        }
        public static int OnLeftPickTrigger
        {
            get
            {
                return ActionManager._OnLeftPickTrigger;
            }
        }
        public static int OnRightPickTrigger
        {
            get
            {
                return ActionManager._OnRightPickTrigger;
            }
        }
        public static int OnCenterPickTrigger
        {
            get
            {
                return ActionManager._OnCenterPickTrigger;
            }
        }
        public static int OnPointerOverTrigger
        {
            get
            {
                return ActionManager._OnPointerOverTrigger;
            }
        }
        public static int OnPointerOutTrigger
        {
            get
            {
                return ActionManager._OnPointerOutTrigger;
            }
        }
        public static int OnEveryFrameTrigger
        {
            get
            {
                return ActionManager._OnEveryFrameTrigger;
            }
        }
        public static int OnIntersectionEnterTrigger
        {
            get
            {
                return ActionManager._OnIntersectionEnterTrigger;
            }
        }
        public static int OnIntersectionExitTrigger
        {
            get
            {
                return ActionManager._OnIntersectionExitTrigger;
            }
        }
        public Array<Action> actions = new Array<Action>();
        private Scene _scene;
        public ActionManager(Scene scene)
        {
            this._scene = scene;
            scene._actionManagers.push(this);
        }
        public virtual void dispose()
        {
            var index = this._scene._actionManagers.indexOf(this);
            if (index > -1)
            {
                this._scene._actionManagers.splice(index, 1);
            }
        }
        public virtual Scene getScene()
        {
            return this._scene;
        }
        public virtual bool hasSpecificTriggers(Array<int> triggers)
        {
            for (var index = 0; index < this.actions.Length; index++)
            {
                var action = this.actions[index];
                if (triggers.indexOf(action.trigger) > -1)
                {
                    return true;
                }
            }
            return false;
        }
        public virtual bool hasPointerTriggers
        {
            get
            {
                for (var index = 0; index < this.actions.Length; index++)
                {
                    var action = this.actions[index];
                    if (action.trigger >= ActionManager._OnPickTrigger && action.trigger <= ActionManager._OnPointerOutTrigger)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public virtual bool hasPickTriggers
        {
            get
            {
                for (var index = 0; index < this.actions.Length; index++)
                {
                    var action = this.actions[index];
                    if (action.trigger >= ActionManager._OnPickTrigger && action.trigger <= ActionManager._OnCenterPickTrigger)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public virtual Action registerAction(Action action)
        {
            if (action.trigger == ActionManager.OnEveryFrameTrigger)
            {
                if (this.getScene().actionManager != this)
                {
                    Tools.Warn("OnEveryFrameTrigger can only be used with scene.actionManager");
                    return null;
                }
            }
            this.actions.push(action);
            action._actionManager = this;
            action._prepare();
            return action;
        }
        public virtual void processTrigger(double trigger, ActionEvent evt)
        {
            for (var index = 0; index < this.actions.Length; index++)
            {
                var action = this.actions[index];
                if (action.trigger == trigger)
                {
                    action._executeCurrent(evt);
                }
            }
        }
        public virtual object _getEffectiveTarget(object target, string propertyPath)
        {
            var properties = propertyPath.Split(".");
            for (var index = 0; index < properties.Length - 1; index++)
            {
                target = target[properties[index]];
            }
            return target;
        }
        public virtual string _getProperty(string propertyPath)
        {
            var properties = propertyPath.Split(".");
            return properties[properties.Length - 1];
        }
    }
}