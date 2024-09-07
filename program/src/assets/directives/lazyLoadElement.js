// lazyLoadElement.js
import 'intersection-observer';
export default {
  beforeMount(el, binding) {
    const callback = entries => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          binding.value();
          observer.unobserve(el);
        }
      });
    };
    const observer = new IntersectionObserver(callback);
    observer.observe(el);
  }
};