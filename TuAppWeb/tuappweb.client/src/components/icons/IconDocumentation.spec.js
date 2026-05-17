import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import IconDocumentation from './IconDocumentation.vue'

describe('IconDocumentation', () => {
  it('renderiza un svg', () => {
    const wrapper = mount(IconDocumentation)
    expect(wrapper.find('svg').exists()).toBe(true)
  })
})
