管理员页面

components为组件

views为视图

router为自己配置的路由

---

整体页面布局为adminPage.vue

其中div.toolbar是筛选栏

div.display-table是显示表格处(我用的是element plus，使用前需要安装并引入)

==注：这里用到了路由，如果要添加组件的话需要相应添加在router中==

我感觉筛选栏和表格(customer_infoTable.vue)的制作总体上相差不大，大家可以在我写好的代码基础上进行修改就可以了

如果有父子组件之间数据的传递可以参考我的Dropdown_gender(子组件)和adminPage.vue(父组件)的写法

----

调整过后整个右边页面一开始都是空白，只有点击左侧侧边栏才会对应显示

----

完成了清空、删除、查询的实现，并把相应的图标配到了文字旁边
