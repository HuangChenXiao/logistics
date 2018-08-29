const getters = {
  token: state => state.user.token,
  side_cat: state => state.com.side_cat,
  user_info: state => state.user.user_info,
  img_code: state => state.com.img_code,
  order_remark: state => state.com.order_remark,
  formattedAddress: state => state.com.formattedAddress,
  longitude: state => state.com.longitude,
  latitude: state => state.com.latitude,
  start_position: state => state.com.start_position,
  isLoading: state => state.com.isLoading,
  loadingText: state => state.com.loadingText,
}
export default getters
