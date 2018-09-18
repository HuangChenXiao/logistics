const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [
    
  { name: 'home', path: '/', component: _import('home/index'), meta: { verifylogin: false } },
  { name: 'audit', path: '/audit', component: _import('home/audit'), meta: { verifylogin: false } },
  { name: 'means', path: '/means', component: _import('home/means'), meta: { verifylogin: false } },
  { name: 'admin_index', path: '/admin_index', component: _import('home/admin_index'), meta: { verifylogin: false } },
  ]