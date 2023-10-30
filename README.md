This code sample provides a leaderboard (score board of match results) ordered by total score or by the most resent matches in case they have equal scores.
The solution is built upon AVL tree with an extra feature to support the second requirement of ordering - matches with equals score.

During investigation I probed several approaches:
1. Splay tree
2. Treeset / Hashset
3. AVL tree

Splay trees are considered as common approach to build score boards. They support insert, delete, search and interesting way of rotation - Zig-Zag.
There're two features of a splay tree that make me look for another approach. 
The first is whatever operation is applied to a splay tree (even search) it makes changes to its structure. In short, if you touch a node, it would become a root node. 
So for a node update, the tree has to be re-balance twice: to seach a node and to put it into the right place.
Summary: Splay trees are good to generate a scoreboard on the fly. So the actual results might be stored in a separate collection. And also a common splay tree doesn't solve the requirement of nodes with equal scores.

Treeset and hashset. These types of collection exist in many languages: Java, C#, C++, Python.
They both look similar: Treeset uses Red-Black tree and hashset uses hashmap. Both use a compare method to order items. 
While that is the easiest way to implement a scoreboard, this approach has limited space for customization, especially when it comes to the multiple nodes with equal score problem.

AVL Tree.
That was picked as a target approach. AVL tree has the same features that a splay tree and doesn't have its limitations.
Also I found a workaround how to solve the equal-score-nodes problem. The solution came from hash tables, where we meet collisions. There're two major aproaches how to solve a collision:
1. Separate Chaning
2. Open Addressing.

In this AVL tree a kind of separate chaning approach was applied.
Each node contains a Score and Value list. The Score property is addressed to the match total score and used in tree balancing. Value list contains at least a single value (match data). 
If an upcoming match has the same score that an existing node has, so that node's value list is updated with that upcoming match data.
Suppose there is a list of matches started at the specified order:
a. Mexico 0 - Canada 5
b. Spain 10 - Brazil 2
c. Germany 2 - France 2
d. Uruguay 6 - Italy 6
e. Argentina 3 - Australia 1

So the tree will look like
                                            5: Value list: ["Mexico 0 - Canada 5"]
                                      /                                                \
4: Value List: ["Germany 2 - France 2", "Argentina 3 - Australia 1"]              12: Value List: ["Spain 10 - Brazil 2", "Uruguay 6 - Italy 6"]

When we call Top method of the Leaderboard class, it would return the following result:
1. Uruguay 6 - Italy 6
2. Spain 10 - Brazil 2
3. Mexico 0 - Canada 5
4. Argentina 3 - Australia 1
5. Germany 2 - France 2
