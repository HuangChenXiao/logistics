const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [

  { name: 'admin-user', path: '/admin-user', component: _import('user/admin-user'), meta: { verifylogin: true } },
  { name: 'user', path: '/user', component: _import('user/index'), meta: { verifylogin: true } },
  { name: 'order', path: '/user/order', component: _import('user/order'), meta: { verifylogin: true } },
  { name: 'route-line', path: '/user/route-line', component: _import('user/route-line'), meta: { verifylogin: true } },
]