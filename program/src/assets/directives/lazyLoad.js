// lazyLoad.js
export default {
  beforeMount(el, binding) {
    // 定义 IntersectionObserver 的回调函数
    const callback = entries => {
      // 遍历所有被观察的元素
      entries.forEach(entry => {
        // 如果元素进入视口
        if (entry.isIntersecting) {
          // 调用绑定函数，执行懒加载后的操作
          binding.value();
          // 停止观察该元素，避免重复触发回调
          observer.unobserve(el);
        }
      });
    };
    // 创建 IntersectionObserver 实例，传入回调函数
    const observer = new IntersectionObserver(callback);
    // 开始观察传入的元素 el
    observer.observe(el);
  }
};