# Mesh Deformation Tutorial
一個簡易的動態網格變形系統開發教學。

![image](https://github.com/naukri7707/MeshDeformationTutorial/assets/50033668/f7bf33f8-75e2-473c-9ba8-a4ef443f3f75)

## Demo

https://github.com/naukri7707/MeshDeformationTutorial/assets/50033668/11f32123-e494-4864-b6bf-5b9777c85521

https://github.com/naukri7707/MeshDeformationTutorial/assets/50033668/d6394a57-aac3-47f9-97c6-043e4a9d5305

## 場景介紹

### 01. CubeBuilder
使用程式碼生成高面數的立方體。

### 02. ClosestPoint
`Collider.ClosestPoint()` 探討，以及為什麼無法應用在變形系統上。

### 03. ProjectOnPlane
動態演示 `Vector3.ProjectOnPlane()` 的使用方法。

### 04. ClosestPointToBox
實作並動態演示能夠計算任意點到碰撞器邊界的最進相交點。

### 05. BoundingBox
視覺化 BoundingBox 及 BoundingBox 的應用場景與效能評估。

### 06. MeshDeformation
實作網格變形系統，以及因應不同情況衍生的解決方案。

### 07. VertexModifier
實作網格變形後處理功能，讓網格能透過掛載不同的後處理腳本產生不同的變形效果。

### 08. ShaderPassLayer
實作動態選擇渲染通道功能，讓模型的面能夠在不同的情況下使用對應的材質。

## Toolkit 安裝與使用
1. 到 [Release](https://github.com/naukri7707/MeshDeformationTutorial/releases) 頁面下載 `MeshDeformationToolkit.unitypackage` 並匯入專案
2. 在要變形的模型上掛上 `DeformableObject` 腳本
3. 在用來變形的工具上掛上 `BoxDeformer` 腳本
4. 調用 `BoxDeformer` 的 `Deform()` 函式進行變形
   > 也可以透過 `DeformOnTriggerStay` 腳本在碰撞時自動調用 `Deform()`。

## 注意事項
- 專案的開發版本為 `Unity 2022.1.19f1` ，部分程式碼使用 C# 9.0 的語法。
- 掛載 `DeformOnTriggerStay` 的物件需要能夠與掛載 `DeformableObject` 的物件碰撞並觸發 `OnTriggerStay()` 才能正常運行
- 如果使用的 `VertexModifier` 或 `ShaderPassLayer` 為帶參數的版本，請參考第七單元的 `MyParameters` 通過實作所需介面建立繫結資料。





