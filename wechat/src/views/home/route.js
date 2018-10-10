const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [
    
  { name: 'home', path: '/index', component: _import('home/index'), meta: { role_code:'002',verifylogin: true } },
  { name: 'audit', path: '/audit', component: _import('home/audit'), meta: { verifylogin: false } },
  { name: 'means', path: '/means', component: _import('home/means'), meta: { verifylogin: false } },
  { name: 'oauth', path: '/', component: _import('home/oauth'), meta: { verifylogin: false } },
  { name: 'web404', path: '/web404', component: _import('home/web404'), meta: { verifylogin: false } },
  //现场管理员
  { name: 'admin-index', path: '/admin-index', component: _import('home/admin-index'), meta: { role_code:'001',verifylogin: true } },
  ]