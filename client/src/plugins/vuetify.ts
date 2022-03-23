import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);


export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#DA3E52',
                secondary: '#E57685',
                accent: '#9A1D2E'

            }
        }
    }
});
