var com = {
    state: {
        isLoading:false,//页面加载loading
        loadingText:'加载中',//loading文字
        side_cat: null,//首页右边分类树
        img_code: null,//验证码
        order_remark: null,
        fullHeight: 0,//当前屏幕高度
        formattedAddress: "正在定位...",
        longitude: null,
        latitude: null,
        start_position:null,
    },
    mutations: {
        SET_SIDE_CAT: (state, side_cat) => {
            state.side_cat = side_cat
        },
        IMG_CODE: (state, img_code) => {
            state.img_code = img_code
        },
        ORDER_REMARK: (state, order_remark) => {
            state.order_remark = order_remark
        },
        FORMATTED_ADDRESS: (state, formattedAddress) => {
            state.formattedAddress = formattedAddress
        },
        LONG_ITUDE: (state, longitude) => {
            state.longitude = longitude
        },
        LAT_ITUDE: (state, latitude) => {
            state.latitude = latitude
        },
        START_POSITION: (state, start_position) => {
            state.start_position = start_position
        },
        ISLOADING: (state, isLoading) => {
            state.isLoading = isLoading
        },
        LOADINGTEXT: (state, loadingText) => {
            state.loadingText = loadingText
        }
    },
    actions: {
        setCatId({ commit }, side_cat) {
            commit('SET_SIDE_CAT', side_cat)
        },
        imgCode({ commit }, img_code) {
            commit('IMG_CODE', img_code)
        },
        orderRemark({ commit }, order_remark) {
            commit('ORDER_REMARK', order_remark)
        },
        setformattedAddress({ commit }, formattedAddress) {
            commit('FORMATTED_ADDRESS', formattedAddress)
        },
        setlongitude({ commit }, longitude) {
            commit('LONG_ITUDE', longitude)
        },
        setlatitude({ commit }, latitude) {
            commit('LAT_ITUDE', latitude)
        },
        setstart_position({ commit }, start_position) {
            commit('START_POSITION', start_position)
        },
        setisLoading({ commit }, isLoading) {
            commit('ISLOADING', isLoading)
        },
        setloadingText({ commit }, loadingText) {
            commit('LOADINGTEXT', loadingText)
        }
    }
}
export default com