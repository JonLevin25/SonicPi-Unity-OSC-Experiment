use_osc "localhost", 7000
use_real_time

live_loop :metronome do
  sleep 1
end

live_loop :bars_4, sync: :metronome do
  sleep 3.9
end


def tribalBase
  use_synth :dull_bell
  with_fx :compressor,
    ##| slope_above: 0,
  slope_below: 0.5, threshold: 0.5 do
    osc "/vibrate"
    play 40
  end
end

def tribalTamborine
  ##| with_fx :bitcrusher, bits: 32, sample_rate: 18000 do
  with_fx :reverb do
    ##| with_fx :compressor, slope_above: 0.2, threshold: 0.1 do
    ##| sample :drum_tom_hi_soft, amp: 0.3, rpitch: -1,
    ##|   attack: 0.1, sustain: 0.5, release: 0.5, decay: 0.5
    ##| end
    osc "/trees/bounce"
    sample TAMBORINE[1], amp: 2
  end
  ##| end
  ##| with_fx :bitcrusher, bits: 6 do
  ##|   play 64, attack: 0.15
  ##| end
  
end

live_loop :drums do
  in_thread do
    tribalBase
    [0.5].each {|x|
      sleep x
      tribalBase
    }
  end
  
  sleep 1.0 / 3
  tribalTamborine
  sleep 5.0 / 12
  tribalTamborine
  
  
  sleep 1
end
