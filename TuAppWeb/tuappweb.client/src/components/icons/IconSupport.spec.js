import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import IconSupport from './IconSupport.vue'

describe('IconSupport', () => {
  it('renderiza un svg', () => {
    const wrapper = mount(IconSupport)
    expect(wrapper.find('svg').exists()).toBe(true)
  })
})
