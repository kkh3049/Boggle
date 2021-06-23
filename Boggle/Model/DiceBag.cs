using System.Collections.Generic;
using System.Linq;

namespace Boggle.Model
{
    public class DiceBag
    {
        private List<string> m_dice;

        public DiceBag(IList<string> dice)
        {
            m_dice = dice.ToList();
        }

        private static readonly string _cubes =
            @"aaafrs
aaeeee
aafirs
adennn
aeeeem
aeegmu
aegmnn
afirsy
bjkqxz
ccenst
ceiilt
ceilpt
ceipst
ddhnot
dhhlor
dhlnor
dhlnor
eiiitt
emottt
ensssu
fiprsy
gorrvw
iprrry
nootuw
ooottu";

        public static DiceBag BasicDiceSet()
        {
            var cubesString = _cubes.Split('\n');
            return new DiceBag(cubesString);
        }
    }
}