## 全局规范

- 所有 `public` 字段、属性、方法都必须要有详细、完整的简体中文 XML DOC 注释。

## 写法

- 若一个字段实际上是各个位的组合，则需要使用 [BitField<T>(...)] 来标记。
- `FixedSizeArrayXX` 类型最后的数字为数组长度，需要为 internal 类型，需要标注 [FixedSizeArray]。
    - 如果是字符串需要在 [FixedSizeArray] 标记中指定 isString 为 true。
    - 与原版不同，命名需要为 `YYYYStorage`，生成器会自动剥离名称结尾的 `Storage`。