const _import = require('@/router/_import_' + process.env.NODE_ENV)
//模块路由
export default [

  { name: 'admin-user', path: '/admin-user', component: _import('user/admin-user'), meta: { role_code:'001',verifylogin: true } },
  { name: 'destination-user', path: '/destination-user', component: _import('user/destination-user'), meta: { role_code:'003',verifylogin: true } },
  { name: 'user', path: '/user', component: _import('user/index'), meta: {role_code:'002', verifylogin: true } },
  { name: 'order', path: '/user/order', component: _import('user/order'), meta: { verifylogin: true } },
  { name: 'driver-count', path: '/user/driver-count', component: _import('user/driver-count'), meta: { verifylogin: true } },
  { name: 'admin-head-tail', path: '/user/admin-head-tail', component: _import('user/admin-head-tail'), meta: { verifylogin: true } },
  { name: 'route-line', path: '/user/route-line', component: _import('user/route-line'), meta: { verifylogin: true } },
  { name: 'tu-wei', path: '/user/tu-wei', component: _import('user/tu-wei'), meta: { role_code:'001',verifylogin: true } },
  { name: 'tu-mudi-wei', path: '/user/tu-mudi-wei', component: _import('user/tu-wei'), meta: { role_code:'003',verifylogin: true } },
  { name: 'boss-user', path: '/user/boss-user', component: _import('user/boss-user'), meta: { role_code:'005',verifylogin: true } },
  { name: 'gong-di', path: '/user/gong-di', component: _import('user/gong-di'), meta: { role_code:'005',verifylogin: true } },
  { name: 'map', path: '/user/map', component: _import('user/map'), meta: {verifylogin: true } },
]