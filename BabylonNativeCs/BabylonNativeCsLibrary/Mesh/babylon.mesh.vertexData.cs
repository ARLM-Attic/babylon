using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BABYLON {
public interface IGetSetVerticesData {
bool isVerticesDataPresent(string kind);
float[] getVerticesData(string kind);
float[] getIndices();
void setVerticesData(string kind, float[] data, bool updatable);
void updateVerticesData(string kind, float[] data, bool updateExtends, bool makeItUnique);
void setIndices(float[] indices);
}
public class VertexData {
public float[] positions;
public float[] normals;
public float[] uvs;
public float[] uv2s;
public float[] colors;
public float[] matricesIndices;
public float[] matricesWeights;
public float[] indices;
public virtual void set(float[] data, string kind) {
switch (kind) {
case BABYLON.VertexBuffer.PositionKind: 
this.positions=data;
break;
case BABYLON.VertexBuffer.NormalKind: 
this.normals=data;
break;
case BABYLON.VertexBuffer.UVKind: 
this.uvs=data;
break;
case BABYLON.VertexBuffer.UV2Kind: 
this.uv2s=data;
break;
case BABYLON.VertexBuffer.ColorKind: 
this.colors=data;
break;
case BABYLON.VertexBuffer.MatricesIndicesKind: 
this.matricesIndices=data;
break;
case BABYLON.VertexBuffer.MatricesWeightsKind: 
this.matricesWeights=data;
break;
}
}
public virtual void applyToMesh(Mesh mesh, bool updatable) {
this._applyTo(mesh, updatable);
}
public virtual void applyToGeometry(Geometry geometry, bool updatable) {
this._applyTo(geometry, updatable);
}
public virtual void updateMesh(Mesh mesh, bool updateExtends, bool makeItUnique) {
this._update(mesh);
}
public virtual void updateGeometry(Geometry geometry, bool updateExtends, bool makeItUnique) {
this._update(geometry);
}
private virtual void _applyTo(IGetSetVerticesData meshOrGeometry, bool updatable) {
if (this.positions) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.PositionKind, this.positions, updatable);
}
if (this.normals) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.NormalKind, this.normals, updatable);
}
if (this.uvs) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.UVKind, this.uvs, updatable);
}
if (this.uv2s) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.UV2Kind, this.uv2s, updatable);
}
if (this.colors) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.ColorKind, this.colors, updatable);
}
if (this.matricesIndices) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.MatricesIndicesKind, this.matricesIndices, updatable);
}
if (this.matricesWeights) 
{
meshOrGeometry.setVerticesData(BABYLON.VertexBuffer.MatricesWeightsKind, this.matricesWeights, updatable);
}
if (this.indices) 
{
meshOrGeometry.setIndices(this.indices);
}
}
private virtual void _update(IGetSetVerticesData meshOrGeometry, bool updateExtends, bool makeItUnique) {
if (this.positions) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.PositionKind, this.positions, updateExtends, makeItUnique);
}
if (this.normals) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.NormalKind, this.normals, updateExtends, makeItUnique);
}
if (this.uvs) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.UVKind, this.uvs, updateExtends, makeItUnique);
}
if (this.uv2s) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.UV2Kind, this.uv2s, updateExtends, makeItUnique);
}
if (this.colors) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.ColorKind, this.colors, updateExtends, makeItUnique);
}
if (this.matricesIndices) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.MatricesIndicesKind, this.matricesIndices, updateExtends, makeItUnique);
}
if (this.matricesWeights) 
{
meshOrGeometry.updateVerticesData(BABYLON.VertexBuffer.MatricesWeightsKind, this.matricesWeights, updateExtends, makeItUnique);
}
if (this.indices) 
{
meshOrGeometry.setIndices(this.indices);
}
}
public virtual void transform(Matrix matrix) {
var transformed = BABYLON.Vector3.Zero();
if (this.positions) 
{
var position = BABYLON.Vector3.Zero();
for (var index = 0;index<this.positions.length;index+=3) 
{
BABYLON.Vector3.FromArrayToRef(this.positions, index, position);
BABYLON.Vector3.TransformCoordinatesToRef(position, matrix, transformed);
this.positions[index]=transformed.x;
this.positions[index+1]=transformed.y;
this.positions[index+2]=transformed.z;
}
}
if (this.normals) 
{
var normal = BABYLON.Vector3.Zero();
for (index=0;index<this.normals.length;index+=3) 
{
BABYLON.Vector3.FromArrayToRef(this.normals, index, normal);
BABYLON.Vector3.TransformNormalToRef(normal, matrix, transformed);
this.normals[index]=transformed.x;
this.normals[index+1]=transformed.y;
this.normals[index+2]=transformed.z;
}
}
}
public virtual void merge(VertexData other) {
if (other.indices) 
{
if (!this.indices) 
{
this.indices=new Array<object>();
}
var offset = (this.positions) ? this.positions.length/3 : 0;
for (var index = 0;index<other.indices.length;index++) 
{
this.indices.push(other.indices[index]+offset);
}
}
if (other.positions) 
{
if (!this.positions) 
{
this.positions=new Array<object>();
}
for (index=0;index<other.positions.length;index++) 
{
this.positions.push(other.positions[index]);
}
}
if (other.normals) 
{
if (!this.normals) 
{
this.normals=new Array<object>();
}
for (index=0;index<other.normals.length;index++) 
{
this.normals.push(other.normals[index]);
}
}
if (other.uvs) 
{
if (!this.uvs) 
{
this.uvs=new Array<object>();
}
for (index=0;index<other.uvs.length;index++) 
{
this.uvs.push(other.uvs[index]);
}
}
if (other.uv2s) 
{
if (!this.uv2s) 
{
this.uv2s=new Array<object>();
}
for (index=0;index<other.uv2s.length;index++) 
{
this.uv2s.push(other.uv2s[index]);
}
}
if (other.matricesIndices) 
{
if (!this.matricesIndices) 
{
this.matricesIndices=new Array<object>();
}
for (index=0;index<other.matricesIndices.length;index++) 
{
this.matricesIndices.push(other.matricesIndices[index]);
}
}
if (other.matricesWeights) 
{
if (!this.matricesWeights) 
{
this.matricesWeights=new Array<object>();
}
for (index=0;index<other.matricesWeights.length;index++) 
{
this.matricesWeights.push(other.matricesWeights[index]);
}
}
if (other.colors) 
{
if (!this.colors) 
{
this.colors=new Array<object>();
}
for (index=0;index<other.colors.length;index++) 
{
this.colors.push(other.colors[index]);
}
}
}
public static virtual VertexData ExtractFromMesh(Mesh mesh) {
return VertexData._ExtractFrom(mesh);
}
public static virtual VertexData ExtractFromGeometry(Geometry geometry) {
return VertexData._ExtractFrom(geometry);
}
private static virtual VertexData _ExtractFrom(IGetSetVerticesData meshOrGeometry) {
var result = new BABYLON.VertexData();
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.PositionKind)) 
{
result.positions=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.PositionKind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.NormalKind)) 
{
result.normals=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.NormalKind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.UVKind)) 
{
result.uvs=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.UVKind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.UV2Kind)) 
{
result.uv2s=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.UV2Kind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.ColorKind)) 
{
result.colors=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.ColorKind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.MatricesIndicesKind)) 
{
result.matricesIndices=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.MatricesIndicesKind);
}
if (meshOrGeometry.isVerticesDataPresent(BABYLON.VertexBuffer.MatricesWeightsKind)) 
{
result.matricesWeights=meshOrGeometry.getVerticesData(BABYLON.VertexBuffer.MatricesWeightsKind);
}
result.indices=meshOrGeometry.getIndices();
return result;
}
public static virtual VertexData CreateBox(float size) {
var normalsSource = new Array<object>();
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
size=size||1;
for (var index = 0;index<normalsSource.length;index++) 
{
var normal = normalsSource[index];
var side1 = new BABYLON.Vector3(normal.y, normal.z, normal.x);
var side2 = BABYLON.Vector3.Cross(normal, side1);
var verticesLength = positions.length/3;
indices.push(verticesLength);
indices.push(verticesLength+1);
indices.push(verticesLength+2);
indices.push(verticesLength);
indices.push(verticesLength+2);
indices.push(verticesLength+3);
var vertex = normal.subtract(side1).subtract(side2).scale(size/2);
positions.push(vertex.x, vertex.y, vertex.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(1.0, 1.0);
vertex=normal.subtract(side1).add(side2).scale(size/2);
positions.push(vertex.x, vertex.y, vertex.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(0.0, 1.0);
vertex=normal.add(side1).add(side2).scale(size/2);
positions.push(vertex.x, vertex.y, vertex.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(0.0, 0.0);
vertex=normal.add(side1).subtract(side2).scale(size/2);
positions.push(vertex.x, vertex.y, vertex.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(1.0, 0.0);
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateSphere(float segments, float diameter) {
segments=segments||32;
diameter=diameter||1;
var radius = diameter/2;
var totalZRotationSteps = 2+segments;
var totalYRotationSteps = 2*totalZRotationSteps;
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
for (var zRotationStep = 0;zRotationStep<=totalZRotationSteps;zRotationStep++) 
{
var normalizedZ = zRotationStep/totalZRotationSteps;
var angleZ = (normalizedZ*Math.PI);
for (var yRotationStep = 0;yRotationStep<=totalYRotationSteps;yRotationStep++) 
{
var normalizedY = yRotationStep/totalYRotationSteps;
var angleY = normalizedY*Math.PI*2;
var rotationZ = BABYLON.Matrix.RotationZ(-angleZ);
var rotationY = BABYLON.Matrix.RotationY(angleY);
var afterRotZ = BABYLON.Vector3.TransformCoordinates(BABYLON.Vector3.Up(), rotationZ);
var complete = BABYLON.Vector3.TransformCoordinates(afterRotZ, rotationY);
var vertex = complete.scale(radius);
var normal = BABYLON.Vector3.Normalize(vertex);
positions.push(vertex.x, vertex.y, vertex.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(normalizedZ, normalizedY);
}
if (zRotationStep>0) 
{
var verticesCount = positions.length/3;
for (var firstIndex = verticesCount-2*(totalYRotationSteps+1);(firstIndex+totalYRotationSteps+2)<verticesCount;firstIndex++) 
{
indices.push((firstIndex));
indices.push((firstIndex+1));
indices.push(firstIndex+totalYRotationSteps+1);
indices.push((firstIndex+totalYRotationSteps+1));
indices.push((firstIndex+1));
indices.push((firstIndex+totalYRotationSteps+2));
}
}
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateCylinder(float height, float diameterTop, float diameterBottom, float tessellation, float subdivisions) {
var radiusTop = diameterTop/2;
var radiusBottom = diameterBottom/2;
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
height=height||1;
diameterTop=diameterTop||0.5;
diameterBottom=diameterBottom||1;
tessellation=tessellation||16;
subdivisions=subdivisions||1;
subdivisions=((subdivisions<1)) ? 1 : subdivisions;
var getCircleVector = (dynamic i) => {
var angle = (i*2.0*Math.PI/tessellation);
var dx = Math.cos(angle);
var dz = Math.sin(angle);
return new BABYLON.Vector3(dx, 0, dz);
}
;
var createCylinderCap = (dynamic isTop) => {
var radius = (isTop) ? radiusTop : radiusBottom;
if (radius==0) 
{
return;
}
var vbase = positions.length/3;
var offset = new BABYLON.Vector3(0, height/2, 0);
var textureScale = new BABYLON.Vector2(0.5, 0.5);
if (!isTop) 
{
offset.scaleInPlace(-1);
textureScale.x=-textureScale.x;
}
for (i=0;i<tessellation;i++) 
{
var circleVector = getCircleVector(i);
var position = circleVector.scale(radius).add(offset);
var textureCoordinate = new BABYLON.Vector2(circleVector.x*textureScale.x+0.5, circleVector.z*textureScale.y+0.5);
positions.push(position.x, position.y, position.z);
uvs.push(textureCoordinate.x, textureCoordinate.y);
}
for (var i = 0;i<tessellation-2;i++) 
{
if (!isTop) 
{
indices.push(vbase);
indices.push(vbase+(i+2)%tessellation);
indices.push(vbase+(i+1)%tessellation);
}
else 
{
indices.push(vbase);
indices.push(vbase+(i+1)%tessellation);
indices.push(vbase+(i+2)%tessellation);
}
}
}
;
var base = new BABYLON.Vector3(0, -1, 0).scale(height/2);
var offset = new BABYLON.Vector3(0, 1, 0).scale(height/subdivisions);
var stride = tessellation+1;
for (var i = 0;i<=tessellation;i++) 
{
var circleVector = getCircleVector(i);
var textureCoordinate = new BABYLON.Vector2(i/tessellation, 0);
var positionradius = radiusBottom;
for (var s = 0;s<=subdivisions;s++) 
{
position=circleVector.scale(radius);
position.addInPlace(base.add(offset.scale(s)));
textureCoordinate.y+=1/subdivisions;
radius+=(radiusTop-radiusBottom)/subdivisions;
positions.push(position.x, position.y, position.z);
uvs.push(textureCoordinate.x, textureCoordinate.y);
}
}
subdivisions+=1;
for (var s = 0;s<subdivisions-1;s++) 
{
for (var i = 0;i<=tessellation;i++) 
{
indices.push(i*subdivisions+s);
indices.push((i*subdivisions+(s+subdivisions))%(stride*subdivisions));
indices.push(i*subdivisions+(s+1));
indices.push(i*subdivisions+(s+1));
indices.push((i*subdivisions+(s+subdivisions))%(stride*subdivisions));
indices.push((i*subdivisions+(s+subdivisions+1))%(stride*subdivisions));
}
}
createCylinderCap(true);
createCylinderCap(false);
BABYLON.VertexData.ComputeNormals(positions, indices, normals);
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual void CreateTorus(dynamic diameter, dynamic thickness, dynamic tessellation) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
diameter=diameter||1;
thickness=thickness||0.5;
tessellation=tessellation||16;
var stride = tessellation+1;
for (var i = 0;i<=tessellation;i++) 
{
var u = i/tessellation;
var outerAngle = i*Math.PI*2.0/tessellation-Math.PI/2.0;
var transform = BABYLON.Matrix.Translation(diameter/2.0, 0, 0).multiply(BABYLON.Matrix.RotationY(outerAngle));
for (var j = 0;j<=tessellation;j++) 
{
var v = 1-j/tessellation;
var innerAngle = j*Math.PI*2.0/tessellation+Math.PI;
var dx = Math.cos(innerAngle);
var dy = Math.sin(innerAngle);
var normal = new BABYLON.Vector3(dx, dy, 0);
var position = normal.scale(thickness/2);
var textureCoordinate = new BABYLON.Vector2(u, v);
position=BABYLON.Vector3.TransformCoordinates(position, transform);
normal=BABYLON.Vector3.TransformNormal(normal, transform);
positions.push(position.x, position.y, position.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(textureCoordinate.x, textureCoordinate.y);
var nextI = (i+1)%stride;
var nextJ = (j+1)%stride;
indices.push(i*stride+j);
indices.push(i*stride+nextJ);
indices.push(nextI*stride+j);
indices.push(i*stride+nextJ);
indices.push(nextI*stride+nextJ);
indices.push(nextI*stride+j);
}
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateLines(Vector3[] points) {
var indices = new Array<object>();
var positions = new Array<object>();
for (var index = 0;index<points.length;index++) 
{
positions.push(points[index].x, points[index].y, points[index].z);
if (index>0) 
{
indices.push(index-1);
indices.push(index);
}
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
return vertexData;
}
public static virtual VertexData CreateGround(float width, float height, float subdivisions) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
var rowcol;
width=width||1;
height=height||1;
subdivisions=subdivisions||1;
for (row=0;row<=subdivisions;row++) 
{
for (col=0;col<=subdivisions;col++) 
{
var position = new BABYLON.Vector3((col*width)/subdivisions-(width/2.0), 0, ((subdivisions-row)*height)/subdivisions-(height/2.0));
var normal = new BABYLON.Vector3(0, 1.0, 0);
positions.push(position.x, position.y, position.z);
normals.push(normal.x, normal.y, normal.z);
uvs.push(col/subdivisions, 1.0-row/subdivisions);
}
}
for (row=0;row<subdivisions;row++) 
{
for (col=0;col<subdivisions;col++) 
{
indices.push(col+1+(row+1)*(subdivisions+1));
indices.push(col+1+row*(subdivisions+1));
indices.push(col+row*(subdivisions+1));
indices.push(col+(row+1)*(subdivisions+1));
indices.push(col+1+(row+1)*(subdivisions+1));
indices.push(col+row*(subdivisions+1));
}
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateTiledGround(float xmin, float zmin, float xmax, float zmax, dynamic subdivisions, dynamic precision) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
var rowcoltileRowtileCol;
subdivisions.h=((subdivisions.w<1)) ? 1 : subdivisions.h;
subdivisions.w=((subdivisions.w<1)) ? 1 : subdivisions.w;
precision.w=((precision.w<1)) ? 1 : precision.w;
precision.h=((precision.h<1)) ? 1 : precision.h;
var tileSize = new dynamic();
for (tileRow=0;tileRow<subdivisions.h;tileRow++) 
{
for (tileCol=0;tileCol<subdivisions.w;tileCol++) 
{
applyTile(xmin+tileCol*tileSize.w, zmin+tileRow*tileSize.h, xmin+(tileCol+1)*tileSize.w, zmin+(tileRow+1)*tileSize.h);
}
}
{
}
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateGroundFromHeightMap(float width, float height, float subdivisions, float minHeight, float maxHeight, Uint8Array buffer, float bufferWidth, float bufferHeight) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
var rowcol;
for (row=0;row<=subdivisions;row++) 
{
for (col=0;col<=subdivisions;col++) 
{
var position = new BABYLON.Vector3((col*width)/subdivisions-(width/2.0), 0, ((subdivisions-row)*height)/subdivisions-(height/2.0));
var heightMapX = (((position.x+width/2)/width)*(bufferWidth-1))|0;
var heightMapY = ((1.0-(position.z+height/2)/height)*(bufferHeight-1))|0;
var pos = (heightMapX+heightMapY*bufferWidth)*4;
var r = buffer[pos]/255.0;
var g = buffer[pos+1]/255.0;
var b = buffer[pos+2]/255.0;
var gradient = r*0.3+g*0.59+b*0.11;
position.y=minHeight+(maxHeight-minHeight)*gradient;
positions.push(position.x, position.y, position.z);
normals.push(0, 0, 0);
uvs.push(col/subdivisions, 1.0-row/subdivisions);
}
}
for (row=0;row<subdivisions;row++) 
{
for (col=0;col<subdivisions;col++) 
{
indices.push(col+1+(row+1)*(subdivisions+1));
indices.push(col+1+row*(subdivisions+1));
indices.push(col+row*(subdivisions+1));
indices.push(col+(row+1)*(subdivisions+1));
indices.push(col+1+(row+1)*(subdivisions+1));
indices.push(col+row*(subdivisions+1));
}
}
BABYLON.VertexData.ComputeNormals(positions, indices, normals);
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreatePlane(float size) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
size=size||1;
var halfSize = size/2.0;
positions.push(-halfSize, -halfSize, 0);
normals.push(0, 0, -1.0);
uvs.push(0.0, 0.0);
positions.push(halfSize, -halfSize, 0);
normals.push(0, 0, -1.0);
uvs.push(1.0, 0.0);
positions.push(halfSize, halfSize, 0);
normals.push(0, 0, -1.0);
uvs.push(1.0, 1.0);
positions.push(-halfSize, halfSize, 0);
normals.push(0, 0, -1.0);
uvs.push(0.0, 1.0);
indices.push(0);
indices.push(1);
indices.push(2);
indices.push(0);
indices.push(2);
indices.push(3);
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual VertexData CreateTorusKnot(float radius, float tube, float radialSegments, float tubularSegments, float p, float q) {
var indices = new Array<object>();
var positions = new Array<object>();
var normals = new Array<object>();
var uvs = new Array<object>();
radius=radius||2;
tube=tube||0.5;
radialSegments=radialSegments||32;
tubularSegments=tubularSegments||32;
p=p||2;
q=q||3;
var getPos = (dynamic angle) => {
var cu = Math.cos(angle);
var su = Math.sin(angle);
var quOverP = q/p*angle;
var cs = Math.cos(quOverP);
var tx = radius*(2+cs)*0.5*cu;
var ty = radius*(2+cs)*su*0.5;
var tz = radius*Math.sin(quOverP)*0.5;
return new BABYLON.Vector3(tx, ty, tz);
}
;
for (var i = 0;i<=radialSegments;i++) 
{
var modI = i%radialSegments;
var u = modI/radialSegments*2*p*Math.PI;
var p1 = getPos(u);
var p2 = getPos(u+0.01);
var tang = p2.subtract(p1);
var n = p2.add(p1);
var bitan = BABYLON.Vector3.Cross(tang, n);
n=BABYLON.Vector3.Cross(bitan, tang);
bitan.normalize();
n.normalize();
for (var j = 0;j<tubularSegments;j++) 
{
var modJ = j%tubularSegments;
var v = modJ/tubularSegments*2*Math.PI;
var cx = -tube*Math.cos(v);
var cy = tube*Math.sin(v);
positions.push(p1.x+cx*n.x+cy*bitan.x);
positions.push(p1.y+cx*n.y+cy*bitan.y);
positions.push(p1.z+cx*n.z+cy*bitan.z);
uvs.push(i/radialSegments);
uvs.push(j/tubularSegments);
}
}
for (i=0;i<radialSegments;i++) 
{
for (j=0;j<tubularSegments;j++) 
{
var jNext = (j+1)%tubularSegments;
var a = i*tubularSegments+j;
var b = (i+1)*tubularSegments+j;
var c = (i+1)*tubularSegments+jNext;
var d = i*tubularSegments+jNext;
indices.push(d);
indices.push(b);
indices.push(a);
indices.push(d);
indices.push(c);
indices.push(b);
}
}
BABYLON.VertexData.ComputeNormals(positions, indices, normals);
var vertexData = new BABYLON.VertexData();
vertexData.indices=indices;
vertexData.positions=positions;
vertexData.normals=normals;
vertexData.uvs=uvs;
return vertexData;
}
public static virtual void ComputeNormals(float[] positions, float[] indices, float[] normals) {
var positionVectors = new Array<object>();
var facesOfVertices = new Array<object>();
var index;
for (index=0;index<positions.length;index+=3) 
{
var vector3 = new BABYLON.Vector3(positions[index], positions[index+1], positions[index+2]);
positionVectors.push(vector3);
facesOfVertices.push(new Array<object>());
}
var facesNormals = new Array<object>();
for (index=0;index<indices.length/3;index++) 
{
var i1 = indices[index*3];
var i2 = indices[index*3+1];
var i3 = indices[index*3+2];
var p1 = positionVectors[i1];
var p2 = positionVectors[i2];
var p3 = positionVectors[i3];
var p1p2 = p1.subtract(p2);
var p3p2 = p3.subtract(p2);
facesNormals[index]=BABYLON.Vector3.Normalize(BABYLON.Vector3.Cross(p1p2, p3p2));
facesOfVertices[i1].push(index);
facesOfVertices[i2].push(index);
facesOfVertices[i3].push(index);
}
for (index=0;index<positionVectors.length;index++) 
{
var faces = facesOfVertices[index];
var normal = BABYLON.Vector3.Zero();
for (var faceIndex = 0;faceIndex<faces.length;faceIndex++) 
{
normal.addInPlace(facesNormals[faces[faceIndex]]);
}
normal=BABYLON.Vector3.Normalize(normal.scale(1.0/faces.length));
normals[index*3]=normal.x;
normals[index*3+1]=normal.y;
normals[index*3+2]=normal.z;
}
}
}
}
