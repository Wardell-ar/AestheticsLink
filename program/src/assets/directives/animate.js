export default {
  inserted(el, binding) {  //el 是被指令绑定的元素 binding 是一个对象，包含了指令的值（即传递给指令的参数）
    const { value } = binding;  //从 binding 对象中提取 value，即传递给指令的值
    const delay = value || 0 ;  //如果没有传递值，则默认延迟时间为 0。
    // IntersectionObserver 是一个可以检测元素是否在视口中的 API。
    // 它需要一个回调函数，当被观察的元素进入或离开视口时，这个回调函数会被调用。
    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {  //isIntersecting 属性表示该元素是否进入视口。
          // 使用 CSS 动画设置元素的样式。flyInBottom 是动画名称。1s 是动画持续时间。
          // ease-out 是动画速度曲线。${delay}s 是动画开始前的延迟时间。
          // both 表示动画在开始和结束时都应用这些样式。
          el.style.animation = `flyInBottom 1s ease-out ${delay}s both`;
          // 当动画开始时，不再观察该元素，以避免重复触发动画。
          observer.unobserve(el);
        }
      });
    }, { threshold: 0.1 }); //threshold 是触发回调的阈值。值为 0.1 表示当元素有 10% 进入视口时就会触发回调。
    observer.observe(el); //观察传递进来的元素 el。
  },
};




