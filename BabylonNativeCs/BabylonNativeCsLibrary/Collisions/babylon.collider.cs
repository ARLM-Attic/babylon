using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BABYLON {
    public class Collider {
        public BABYLON.Vector3 radius = new BABYLON.Vector3(1, 1, 1);
        public float retry = 0;
        public Vector3 velocity;
        public Vector3 basePoint;
        public float epsilon;
        public bool collisionFound;
        public float velocityWorldLength;
        public null basePointWorld = BABYLON.Vector3.Zero();
        public null velocityWorld = BABYLON.Vector3.Zero();
        public null normalizedVelocity = BABYLON.Vector3.Zero();
        public Vector3 initialVelocity;
        public Vector3 initialPosition;
        public float nearestDistance;
        public Vector3 intersectionPoint;
        public AbstractMesh collidedMesh;
        private null _collisionPoint = BABYLON.Vector3.Zero();
        private null _planeIntersectionPoint = BABYLON.Vector3.Zero();
        private null _tempVector = BABYLON.Vector3.Zero();
        private null _tempVector2 = BABYLON.Vector3.Zero();
        private null _tempVector3 = BABYLON.Vector3.Zero();
        private null _tempVector4 = BABYLON.Vector3.Zero();
        private null _edge = BABYLON.Vector3.Zero();
        private null _baseToVertex = BABYLON.Vector3.Zero();
        private null _destinationPoint = BABYLON.Vector3.Zero();
        private null _slidePlaneNormal = BABYLON.Vector3.Zero();
        private null _displacementVector = BABYLON.Vector3.Zero();
        public virtual void _initialize(Vector3 source, Vector3 dir, float e) {
            this.velocity = dir;
            BABYLON.Vector3.NormalizeToRef(dir, this.normalizedVelocity);
            this.basePoint = source;
            source.multiplyToRef(this.radius, this.basePointWorld);
            dir.multiplyToRef(this.radius, this.velocityWorld);
            this.velocityWorldLength = this.velocityWorld.Length();
            this.epsilon = e;
            this.collisionFound = false;
        }
        public virtual bool _checkPointInTriangle(Vector3 point, Vector3 pa, Vector3 pb, Vector3 pc, Vector3 n) {
            pa.subtractToRef(point, this._tempVector);
            pb.subtractToRef(point, this._tempVector2);
            BABYLON.Vector3.CrossToRef(this._tempVector, this._tempVector2, this._tempVector4);
            var d = BABYLON.Vector3.Dot(this._tempVector4, n);
            if (d < 0)
                return false;
            pc.subtractToRef(point, this._tempVector3);
            BABYLON.Vector3.CrossToRef(this._tempVector2, this._tempVector3, this._tempVector4);
            d = BABYLON.Vector3.Dot(this._tempVector4, n);
            if (d < 0)
                return false;
            BABYLON.Vector3.CrossToRef(this._tempVector3, this._tempVector, this._tempVector4);
            d = BABYLON.Vector3.Dot(this._tempVector4, n);
            return d >= 0;
        }
        public virtual bool _canDoCollision(Vector3 sphereCenter, float sphereRadius, Vector3 vecMin, Vector3 vecMax) {
            var distance = BABYLON.Vector3.Distance(this.basePointWorld, sphereCenter);
            var max = Math.max(this.radius.x, this.radius.y, this.radius.z);
            if (distance > this.velocityWorldLength + max + sphereRadius) {
                return false;
            }
            if (!intersectBoxAASphere(vecMin, vecMax, this.basePointWorld, this.velocityWorldLength + max))
                return false;
            return true;
        }
        public virtual void _testTriangle(float faceIndex, SubMesh subMesh, Vector3 p1, Vector3 p2, Vector3 p3) {
            var t0;
            var embeddedInPlane = false;
            if (!subMesh._trianglePlanes) {
                subMesh._trianglePlanes = new Array < object > ();
            }
            if (!subMesh._trianglePlanes[faceIndex]) {
                subMesh._trianglePlanes[faceIndex] = new BABYLON.Plane(0, 0, 0, 0);
                subMesh._trianglePlanes[faceIndex].copyFromPoints(p1, p2, p3);
            }
            var trianglePlane = subMesh._trianglePlanes[faceIndex];
            if ((!subMesh.getMaterial()) && !trianglePlane.isFrontFacingTo(this.normalizedVelocity, 0))
                return;
            var signedDistToTrianglePlane = trianglePlane.signedDistanceTo(this.basePoint);
            var normalDotVelocity = BABYLON.Vector3.Dot(trianglePlane.normal, this.velocity);
            if (normalDotVelocity == 0) {
                if (Math.abs(signedDistToTrianglePlane) >= 1.0)
                    return;
                embeddedInPlane = true;
                t0 = 0;
            } else {
                t0 = (-1.0 - signedDistToTrianglePlane) / normalDotVelocity;
                var t1 = (1.0 - signedDistToTrianglePlane) / normalDotVelocity;
                if (t0 > t1) {
                    var temp = t1;
                    t1 = t0;
                    t0 = temp;
                }
                if (t0 > 1.0 || t1 < 0.0)
                    return;
                if (t0 < 0)
                    t0 = 0;
                if (t0 > 1.0)
                    t0 = 1.0;
            }
            this._collisionPoint.copyFromFloats(0, 0, 0);
            var found = false;
            var t = 1.0;
            if (!embeddedInPlane) {
                this.basePoint.subtractToRef(trianglePlane.normal, this._planeIntersectionPoint);
                this.velocity.scaleToRef(t0, this._tempVector);
                this._planeIntersectionPoint.addInPlace(this._tempVector);
                if (this._checkPointInTriangle(this._planeIntersectionPoint, p1, p2, p3, trianglePlane.normal)) {
                    found = true;
                    t = t0;
                    this._collisionPoint.copyFrom(this._planeIntersectionPoint);
                }
            }
            if (!found) {
                var velocitySquaredLength = this.velocity.LengthSquared();
                var a = velocitySquaredLength;
                this.basePoint.subtractToRef(p1, this._tempVector);
                var b = 2.0 * (BABYLON.Vector3.Dot(this.velocity, this._tempVector));
                var c = this._tempVector.LengthSquared() - 1.0;
                var lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    t = lowestRoot.root;
                    found = true;
                    this._collisionPoint.copyFrom(p1);
                }
                this.basePoint.subtractToRef(p2, this._tempVector);
                b = 2.0 * (BABYLON.Vector3.Dot(this.velocity, this._tempVector));
                c = this._tempVector.LengthSquared() - 1.0;
                lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    t = lowestRoot.root;
                    found = true;
                    this._collisionPoint.copyFrom(p2);
                }
                this.basePoint.subtractToRef(p3, this._tempVector);
                b = 2.0 * (BABYLON.Vector3.Dot(this.velocity, this._tempVector));
                c = this._tempVector.LengthSquared() - 1.0;
                lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    t = lowestRoot.root;
                    found = true;
                    this._collisionPoint.copyFrom(p3);
                }
                p2.subtractToRef(p1, this._edge);
                p1.subtractToRef(this.basePoint, this._baseToVertex);
                var edgeSquaredLength = this._edge.LengthSquared();
                var edgeDotVelocity = BABYLON.Vector3.Dot(this._edge, this.velocity);
                var edgeDotBaseToVertex = BABYLON.Vector3.Dot(this._edge, this._baseToVertex);
                a = edgeSquaredLength * (-velocitySquaredLength) + edgeDotVelocity * edgeDotVelocity;
                b = edgeSquaredLength * (2.0 * BABYLON.Vector3.Dot(this.velocity, this._baseToVertex)) - 2.0 * edgeDotVelocity * edgeDotBaseToVertex;
                c = edgeSquaredLength * (1.0 - this._baseToVertex.LengthSquared()) + edgeDotBaseToVertex * edgeDotBaseToVertex;
                lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    var f = (edgeDotVelocity * lowestRoot.root - edgeDotBaseToVertex) / edgeSquaredLength;
                    if (f >= 0.0 && f <= 1.0) {
                        t = lowestRoot.root;
                        found = true;
                        this._edge.scaleInPlace(f);
                        p1.addToRef(this._edge, this._collisionPoint);
                    }
                }
                p3.subtractToRef(p2, this._edge);
                p2.subtractToRef(this.basePoint, this._baseToVertex);
                edgeSquaredLength = this._edge.LengthSquared();
                edgeDotVelocity = BABYLON.Vector3.Dot(this._edge, this.velocity);
                edgeDotBaseToVertex = BABYLON.Vector3.Dot(this._edge, this._baseToVertex);
                a = edgeSquaredLength * (-velocitySquaredLength) + edgeDotVelocity * edgeDotVelocity;
                b = edgeSquaredLength * (2.0 * BABYLON.Vector3.Dot(this.velocity, this._baseToVertex)) - 2.0 * edgeDotVelocity * edgeDotBaseToVertex;
                c = edgeSquaredLength * (1.0 - this._baseToVertex.LengthSquared()) + edgeDotBaseToVertex * edgeDotBaseToVertex;
                lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    f = (edgeDotVelocity * lowestRoot.root - edgeDotBaseToVertex) / edgeSquaredLength;
                    if (f >= 0.0 && f <= 1.0) {
                        t = lowestRoot.root;
                        found = true;
                        this._edge.scaleInPlace(f);
                        p2.addToRef(this._edge, this._collisionPoint);
                    }
                }
                p1.subtractToRef(p3, this._edge);
                p3.subtractToRef(this.basePoint, this._baseToVertex);
                edgeSquaredLength = this._edge.LengthSquared();
                edgeDotVelocity = BABYLON.Vector3.Dot(this._edge, this.velocity);
                edgeDotBaseToVertex = BABYLON.Vector3.Dot(this._edge, this._baseToVertex);
                a = edgeSquaredLength * (-velocitySquaredLength) + edgeDotVelocity * edgeDotVelocity;
                b = edgeSquaredLength * (2.0 * BABYLON.Vector3.Dot(this.velocity, this._baseToVertex)) - 2.0 * edgeDotVelocity * edgeDotBaseToVertex;
                c = edgeSquaredLength * (1.0 - this._baseToVertex.LengthSquared()) + edgeDotBaseToVertex * edgeDotBaseToVertex;
                lowestRoot = getLowestRoot(a, b, c, t);
                if (lowestRoot.found) {
                    f = (edgeDotVelocity * lowestRoot.root - edgeDotBaseToVertex) / edgeSquaredLength;
                    if (f >= 0.0 && f <= 1.0) {
                        t = lowestRoot.root;
                        found = true;
                        this._edge.scaleInPlace(f);
                        p3.addToRef(this._edge, this._collisionPoint);
                    }
                }
            }
            if (found) {
                var distToCollision = t * this.velocity.Length();
                if (!this.collisionFound || distToCollision < this.nearestDistance) {
                    if (!this.intersectionPoint) {
                        this.intersectionPoint = this._collisionPoint.clone();
                    } else {
                        this.intersectionPoint.copyFrom(this._collisionPoint);
                    }
                    this.nearestDistance = distToCollision;
                    this.collisionFound = true;
                    this.collidedMesh = subMesh.getMesh();
                }
            }
        }
        public virtual void _collide(object subMesh, Array < Vector3 > pts, Array < float > indices, float indexStart, float indexEnd, float decal) {
            for (var i = indexStart; i < indexEnd; i += 3) {
                var p1 = pts[indices[i] - decal];
                var p2 = pts[indices[i + 1] - decal];
                var p3 = pts[indices[i + 2] - decal];
                this._testTriangle(i, subMesh, p3, p2, p1);
            }
        }
        public virtual void _getResponse(Vector3 pos, Vector3 vel) {
            pos.addToRef(vel, this._destinationPoint);
            vel.scaleInPlace((this.nearestDistance / vel.Length()));
            this.basePoint.addToRef(vel, pos);
            pos.subtractToRef(this.intersectionPoint, this._slidePlaneNormal);
            this._slidePlaneNormal.normalize();
            this._slidePlaneNormal.scaleToRef(this.epsilon, this._displacementVector);
            pos.addInPlace(this._displacementVector);
            this.intersectionPoint.addInPlace(this._displacementVector);
            this._slidePlaneNormal.scaleInPlace(BABYLON.Plane.SignedDistanceToPlaneFromPositionAndNormal(this.intersectionPoint, this._slidePlaneNormal, this._destinationPoint));
            this._destinationPoint.subtractInPlace(this._slidePlaneNormal);
            this._destinationPoint.subtractToRef(this.intersectionPoint, vel);
        }
    }
}