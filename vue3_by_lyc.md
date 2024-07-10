# vue简介

渐进式 `JavaScript` 框架，易学易用，性能出色，适用于场景丰富的Web前端框架

基于标准的 `HTML` 、 `CSS` 、 `JavaScript` 进行构建，用于开发用户界面



# vue开发准备

熟练适用命令行 `windows + R` 打开命令行

需要安装 `15.0` 或更高版本的 `Node.js`

创建 `vue` 项目 —— `npm init vue@latest`

然后设置项目名称、选择项目选项，并输入一下三个命令启动项目：

`cd <项目文件名>`

`npm install`

`npm run dev`

命令结束后，给出项目对应的网页地址，可以在本机访问



# 开发环境

建议使用 `VS Code`



# vue项目的目录结构

`.vscode`				——VSCode工具的配置文件夹

`node_modules`		      ——Vue项目的运行依赖文件夹

`public` 				 ——资源文件夹

`src`					 ——源码文件夹

`.gitignore`			   ——git忽略文件

`index.html`			   ——入口HTML文件

`package.json`		       ——信息描述文件

`README.md`			     ——注释文件

`vite.config.js`  		 ——Vue配置文件



# 文本插值语法

双括号绑定文本

使用 `v-html` 来识别 `HTML` 文本

```vue
<template>
  <h3>模板语法</h3>
  <p>{{ msg }}</p>
  <p v-html="rawHTML"></p>
</template>

<script>
export default{
  data(){
    return{
      msg: "神奇的语法",
      rawHTML: "<a href='http://itvaizhan.com'>这是一个链接</a>"
    }
  }
}
</script>
```



# 属性绑定

使用 `v-bind` 进行属性绑定，其中 `v-bind:` 可以简记为 `:`

```vue
<template>
  <div v-bind:class="msg" v-bind:id="myid">属性绑定测试</div>
</template>
<script>
export default{
  data(){
    return{
      myclass: "active",
      myid: 'hhh'
    }
  }
}
</script>
```



==**上面的文本绑定与属性绑定的好处在于可以实现内容的动态改变**==



# 条件渲染

对于复合条件的块才进行渲染

使用 `v-if` 、 `v-else-if` 、 `v-else` 指令

```vue
<template>
    <h3>条件渲染</h3>
    <div v-if="flag1">你能看见我吗？</div>
    <div v-else-if="flag2">看不到他，就看看我吧</div>
    <div v-else>看不到他们，就看看我吧</div>
</template>
<script>
export default{
    data(){
        return{
            flag1: false,
            flag2: false
        }
    }
}
</script>
```



# 列表渲染

使用 `v-for` 指令实现列表项的渲染，也适用于遍历对象

绑定 `key` 属性可以提高列表元素换位切换的性能

```vue
<template>
    <h3>列表渲染</h3>
    <p v-for="(name,index) in names" v-bind:key="index">{{ name }}-{{ index }}</p>
    <p v-for="(value,key,index) in obj" v-bind:key="index">{{ index }}-{{ key }}: {{ value }}</p>
</template>
<script>
export default{
    data(){
        return{
            names: ["a", "b", "c"],
            obj: {
                s_id: 2252550,
                age: 20,
                sex: 'man'
            }
        }
    }
}
</script>
```



# 事件处理

使用 `v-on` 指令来监听 `DOM` 事件，并在触发事件时执行对应的 `JavaScript` 

其中 `v-on:` 可以用 `@` 来简记

- **使用内联的方式**

```vue
<template>
    <h3>内联事件处理</h3>
    <button @click="count++">Add 1</button>
    <button @click="count = 0">Clear</button>
    <p>Count is: {{ count }}</p>
</template>
<script>
export default{
    data(){
        return{
            count: 0
        }
    }
}
</script>
```



- **使用函数的方式**（推荐使用）

函数放在 `methods` 中，在函数中获取 `data` 数据需要使用 `this.`

```vue
<template>
    <h3>方法事件处理</h3>
    <button @click="addCount">Add 1</button>
    <button @click="clearCount">Clear</button>
    <p>Count is: {{ count }}</p>
</template>
<script>
export default{
    // 数据信息
    data(){
        return{
            count: 0
        }
    },

    // 函数方法
    methods: {
        addCount(){
            this.count++;
        },
        clearCount(){
            this.count=0;
        }
    }
}
</script>
```



# 事件参数

可以为函数设计参数，其中 `$event` 表示事件对象

```vue
<template>
    <h3>事件传参</h3>
    <a @click="printname(name,$event)" v-for="(name,index) in names" :key="index">{{ name }}</a>
</template>
<script>
export default{
    data(){
        return {
            names: ["Mark", "Eric", "Alice"]
        }
    },

    methods: {
        printname(name,e){
            console.log(name);
        }
    }
}
</script>
```



# 事件修饰符

对事件进行控制，最常用的就是 `.stop` （阻止事件发生）和 `.prevent`（不让子事件冒泡）

```vue
<template>
    <h3>事件修饰符</h3>
    <a @click.prevent="click1" href="12345">prevent测试</a>
    <div @click="click2">
        <p @click.stop="click3">stop测试</p>
    </div>
</template>
<script>
export default{
    data(){
    },

    methods: {
        click1(){
            console.log('成功点击超链接');
        },
        click2(){
            console.log('div外层事件触发');
        },
        click3(){
            console.log('p内层事件触发');
        }
    }
}
</script>
```



# 计算属性

将复杂的计算式放在脚本中，而不是在模板中

模板中是需要引用计算属性的名称即可

```vue
<template>
    <h3>{{ obj.id }}</h3>
    <p>{{ IsEmpty }}</p>
</template>
<script>
export default{
    // 存放数据
    data(){
        return{
            obj: {
                id: '2252550',
                content: ['a','b','c']
            }
        }
    },

    // 存放计算属性值
    computed:{
        IsEmpty(){
            return this.obj.content.length === 0 ? 'Yes' : 'No'
        }
    }
}
</script>
```



# Class绑定

`style` 绑定与之类似

```vue
<template>
    <p :class="{'active':isActive}">Class样式绑定</p>
</template>
<script>
export default{
    data(){
        return{
            isActive: true
        }
    }
}
</script>

<style>
    .active{
        color:red;
        font-size: 30px;
    }
</style>
```



# 侦听器

只能侦听在 `data` 中声明的数据

监听函数是在数据发生改变后就会执行的，监听器的名称就是数据名，其中函数的固定参数为：

`oldValue` —— 改变前的数据值

`newValue` —— 改变后的数据值

```vue
<template>
    <h3>侦听器</h3>
    <p>{{ msg }}</p>
    <button @click="updatemsg">修改数据</button>
</template>
<script>
export default{
    // data中声明的数据才是可响应的响应式数据
    data(){
        return{
            msg: 'HELLO'
        }
    },

    methods:{
        updatemsg(){
            if(this.msg === 'HELLO'){
                this.msg = 'BYE';
            }
            else{
                this.msg = 'HELLO';
            }
        }
    },

    // 监听器，用于监听data中声明的数据
    watch:{
        // 其中
        // newValue是改变后的数据值
        // oldValue是改变前的数据值
        msg(newValue,oldValue){ // 当msg发生改变，就运行一下函数体
            console.log("msg发生改变!");
            console.log("msg改变前为：" + oldValue);
            console.log("msg改变后为：" + newValue);
        }
    }
}
</script>
```



# 表单输入内容的绑定

```vue
<template>
    <h3>表单输入绑定</h3>
    <form>
        <input type="text" v-model="msg">
        <p>{{ msg }}</p>

        <input type="checkbox" id="option1" v-model="checked">
        <label for="option1">{{ checked }}</label>
    </form>
</template>
<script>

export default{
    data(){
        return{
            msg: "", // 实时获取表单输入的内容，存放在msg中
            checked: false
        }
    }
}

</script>
```



# 获取DOM节点进行操作

对标签设置唯一的 `ref` 标识，在后续要使用 `DOM` 节点时使用 `this.$refs.<ref标识>` 来获取 `DOM` 节点

```vue
<template>
    <div ref="dom1">哈哈哈</div>
    <input type="text" v-model="msg">
</template>
<script>
export default{
    data(){
        return{
            msg: ""
        }
    },
    methods:{

    },
    watch:{
        msg(newValue,oldValue){
            this.$refs.dom1.innerHTML = newValue;
        }
    }
}
</script>
```



# script标签的组成

```vue
<script>
    
import .......  // 组件引入
    
export default{
    data(){
        return{ // 数据
            msg: "组件基本组成"
        }
    },
    methods:{ // 函数方法

    },
    computed:{ // 计算属性

    },
    watch:{ // 数据侦听器

    },
    components:{ // 组件注入

    }
}

</script>
```



# 组件组成

组件就是 `.vue` 文件，最大的优势就是可复用

组件结构如下：

```vue
<template>
承载HTML标签
</template>
<script>
js逻辑
</script>

<!-- scoped属性可以让该样式只在当前组件中生效，不加的话，这个就是一个全局样式 -->
<style scoped>
样式
</style>
```



# 组件的局部注入

- 引入组件
- 组件注入
- 显示组件

```vue
<template>
  <!-- 显示组件 -->
  <mycpnt />
</template>
<script>
  // 引入组件
import mycpnt from "./components/cpnt1.vue"

  // 组件注入
export default{
  components:{
    mycpnt
  }
}
</script>
<style scoped>

</style>
```



# 组件的嵌套关系

![image-20240710110609927](assets/image-20240710110609927.png)

不同组件之间可以通过嵌套的关系来组织界面结构，使代码编写、文件组织更加清晰

详见项目 `vue-cpnt-qt`

嵌套关系为：

![143553776bee60655496c99521495e7](assets/143553776bee60655496c99521495e7.jpg)



# 组件的全局注入（不推荐）

在 `main.js` 中进行注入操作，之后在所有的组件中想要使用该组件就可以直接使用

```js
import { createApp } from 'vue'
import App from './App.vue'
import Header from "./pages/Header.vue"

const app = createApp(App)

// 在这进行注入，第一个参数是之后使用时的名字，第二个参数是引入的组件名
app.component("Header",Header);

app.mount('#app')
```



# 组件传递数据

通过 `props` 来实现组件之间的数据传递，只能是父组件传递数据给子组件

`props` 传递给子组件的数据是不能被子组件修改的，对子组件来说是只读的



父组件在使用子组件标签时，通过使用自定义属性来设置传递给子组件的数据

传递的数据可以是静态的，如 `data1` 和 `data2` ，也可以是动态的，如 `data3`

```vue
<Child data1="Parent的第二个数据" data2="Parent的第二个数据" :data3="msg"/>
```



子组件通过 `props` 获取数据，并可以通过双括号插值使用：

```vue
<template>
    <h3>Child</h3>
    <p>{{ data1 }}</p>
    <p>{{ data2 }}</p>
    <p>{{ data3 }}</p>

</template>
<script>
export default{
    props: ["data1","data2","data3"]  // 其中data3为Parent的动态数据
}
</script>
```



# 组件传递数据类型

通过传递动态数据的方式（使用  `v-bind:` 加自定义数据属性即可）可以实现各种数据类型的传递



# props数据校验

对于从父组件传来的数据进行校验

```vue
<script>
export default{
    props:{
        
		data1:{
			type:String,   // 固定传输数据的类型
			required: true  // 要求父组件必须传这个数据
		},
        
		data2:{
			type:[String,Number],   // 固定传输数据的类型
			default: 0    // 若父组件没有传输data2，则显示默认值
		},
        
		data3{
			type:[Array,Object],
			default(){       // 数组或对象类型的数据的默认值需要通过函数的形式给出
				return ["空"];
			}
		}
        
	}
}
</script>
```



# 子组件传递数据给父组件

通过自定义事件来实现

在子组件中由于触发每个事件，可以在事件的函数中，通过调用 `this.$emit` 来触发父组件的自定义事件，并同时传递数据

```vue
<template>
    <h3>Child</h3>
    <button @click="clickEventHandle">向父组件传递数据</button>
</template>
<script>
export default{
    methods:{
        clickEventHandle(){
            // 第一个参数是子组件事件触发的父组件事件的名称
            // 第二个参数是子组件要发给父组件的数据
            this.$emit("parentEvent","Child数据");
        }
    }
}
</script>
```



父组件在使用子组件标签时，定义好事件，并通过事件函数的参数来接收子组件传递来的数据，无法对于子组件的数据进行改变，但能用子组件的数据来改变自己的数据

```vue
<template>
    <h3>组件事件</h3>
    <p>{{ msg }}</p>
    <br>
    <br>
    <Child @parentEvent="getHandle"/>
</template>
<script>
import Child from "./Child.vue"
export default{
    data(){
        return{
            msg:""
        }
    },
    components:{
        Child
    },
    methods:{
        // 这里的data就是子组件传来的数据
        getHandle(data){
            this.msg=data;  // 将本地的数据改为子组件传递来的数据
        }
    }
}
</script>
```



# 数据传递与v-model的配合

详见项目 `iteract-model`

将搜索组件作为 `Main` 组件的子组件，对于搜索组件的搜索内容通过 `v-model` 进行实时获取

并通过数据侦听器来实时将新改变的值发送给父组件 `Main` 

`Main` 组件通过自定义事件来接收数据，并将其赋值给自己组件的数据并加以渲染显示



# 插槽slot

使用插槽可以通过父组件将模板信息传递给子组件

在父组件中通过双尖括号使用子组件标签，内部内容就是需要传递的模板信息

通过 `v-slot` 来给插槽命名

其中 `v-slot:` 可以用 `#` 来简记

当插槽需要使用到子组件的数据时，需要在命名后面加等号来接收一个对象类型数据，通过对象类型数据来点出子组件传递的数据

```vue
<template>

  <!-- 使用子组件，并向子组件发送模板信息 -->
  <SlotBase>
    <template v-slot:slot1>
      <p>这是第一个插槽内容</p>
    </template>
    <template v-slot:slot2>
      <p>这是第二个插槽内容</p>
    </template>
    <template #slot3="slotProps">
      <p>{{ msg }}-{{ slotProps.data }}</p>
    </template>
  </SlotBase>

</template>
<script>
import SlotBase from "./components/SlotBase.vue";
export default{
  components:{
    SlotBase
  },
  data(){
    return{
      msg: "父组件内容显示"
    }
  }
}
</script>
```



在子组件中通过 `slot` 标签来渲染显示父组件传来的模板信息，通过 `name` 来指定使用哪一个插槽

子组件传递数据只需要在使用对应的 `slot` 标签中添加自定义的属性（要用 `v-bind` 进行绑定）即可，属性名为父组件使用时的数据名

```vue
<template>
  <h3>插槽基础知识</h3>
  <slot name="slot1"></slot>  <!-- 获取父组件传来的模板信息并渲染显示 -->
  <slot name="slot2"></slot>
  <slot name="slot3" :data="childmsg"></slot>
</template>
<script>
export default{
  data(){
    return {
      childmsg: "子组件内容显示"
    }
  }
}
</script>
```

**==若插槽内容是动态的，则在父组件中写给子组件的插槽内容只能访问父组件的动态数据==**

**==slot加括号中也可以加内容，加的是默认内容，若父组件并没有传递插槽给子组件，子组件就显示默认内容==**



# 生命周期函数

在一个周期中，到达某个阶段就自动执行的函数

函数的定义不需要放在 `methods` 中，直接在 `export default` 中定义即可

在 `mounted` 阶段，组件的 `DOM` 节点才建立好，可以通过 `this.$refs` 来获取各种 `DOM` 节点

```vue
<template>
  <h3>组件的生命周期</h3>
  <p>{{ a }}</p>
  <button @click="Addone">加一</button>
  <button @click="Clear">清零</button>
</template>
<script>

/**
 * 生命周期函数 —— 随着组件的生命周期自动触发的函数
 *  创建期：beforeCreate    created
 *  挂载期：beforeMount     mounted
 *  更新期：beforeUpdate    updated
 *  销毁期：beforeUnmount   unmounted
 * 
 */


export default{
  data(){
    return{
      a: 0
    }
  },
  methods:{
    Addone(){
      this.a++;
    },
    Clear(){
      this.a=0;
    }
  },
  beforeCreate(){
    console.log("准备创建组件");
  },
  created(){
    console.log("组件创建完成");
  },
  beforeMount(){
    console.log("准备渲染组件");
  },
  mounted(){
    console.log("组件渲染完成");
  },
  beforeUpdate(){
    console.log("准备更新数据");
  },
  updated(){
    console.log("数据更新完成");
  },
  beforeUnmount(){
    console.log("准备销毁组件");
  },
  unmounted(){
    console.log("组件销毁完成");
  }
}
</script>
```



# 组件的动态切换

将组件设置为父组件的数据，通过 `component` 双尖括号来确定显示的组件是什么

确定的条件是 `is` 属性，其值就是显示的组件名称

```vue
<template>
  <component :is="cpntType"></component>
  <button @click="changeCPNT">切换组件</button>
</template>

<script>
import cpnt1 from "./components/cpnt1.vue"
import cpnt2 from "./components/cpnt2.vue"

export default{
  data(){
    return{
      cpntType: "cpnt1"
    }
  },
  components:{
    cpnt1,
    cpnt2
  },
  methods:{
    changeCPNT(){
      this.cpntType = this.cpntType === "cpnt1" ? "cpnt2" : "cpnt1";
    }
  }
}

</script>
```

==**在切换组件后，被切换的组件会销毁，如果再切换回来，原来改变的新数据都会被恢复为原本的数据**==

==**若需要组件被动态切换后不被销毁，则需要在** `component` **标签外嵌套一个** `keep-alive` **标签**==

```vue
<keep-alive>
	<component :is="cpntType"></component>
</keep-alive>
```



# 异步组件

异步加载组件，在需要使用到组件的时候再加载组件，异步组件的引入方式：

```vue
<script>
import cpnt1 from "./components/cpnt1.vue"  // 组件的直接引入方式

// 组件的异步引入方式
import { defineAsyncComponent } from 'vue'  // 先引入设置异步组件的函数方法
const cpnt2 = defineAsyncComponent(() => import("./components/cpnt2.vue"));

export default{
    components: {  // 注入方式是相同的
        cpnt1,
        cpnt2
    }
}
    

</script>
```



# 依赖注入

`prop` 方法只能实现一级一级的数据传递，使用 `provide` 方法来发送数据， `inject` 方法来获取数据可以实现从祖宗组件直接传递数据给后代组件

```vue
<template>
  <h3>祖宗</h3>
  <Parent/>
</template>
<script>
import Parent from "./components/Parent.vue"
export default{
  components:{
    Parent
  },
  data(){
    return{
      msg2: "hhhh"
    }
  },
  provide(){  // 使用函数形式，可以传递静态数据或者是动态数据，左边标识数据名
    return{
      msg1: "爷爷的财产",
      msg2: this.msg2
    }
  }
}
</script>
```

后代组件使用 `inject` 来接收数据名的数组：

```vue
<template>
    <h3>子组件</h3>
    <p>接收到: {{ childmsg1 }}</p>
    <p>接收到: {{ childmsg2 }}</p>
</template>
<script>
export default{
    inject: ["msg1","msg2"],
    data(){  
        return{   // 可以直接将接收到的数据放在后代组件的数据中
            childmsg1: this.msg1,
            childmsg2: this.msg2
        }
    }
}
</script>
```



# vue执行渲染流程

通过 `createApp` 函数来创建一个 `vue` 的实例对象，每一个 `vue` 项目有且只有一个实例对象

在 `main.js` 中的使用如下：

```js
import { createApp } from 'vue'   // 引入createApp方法
import App from "./App.vue"  // 引入根组件

const app = createApp(App);   // 根据根组件创建实例对象

app.mount('#app');   // 挂载在id为"app"的DOM节点上（index.html文档中）
```

`vue` 文件会被编译为一个 `main.js` 文件，最后在 `index.html` 文件中引入 `main.js` 执行

浏览器真正执行的文件是 `index.html`



# assets文件应用

在 `src` 文件夹中还有一个 `assets` 文件夹，用于存放公共资源，比如图片、 `CSS` 样式.....





