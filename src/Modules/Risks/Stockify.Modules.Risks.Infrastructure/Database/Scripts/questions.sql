-- Create extension for UUID generation
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- Insert questions
INSERT INTO questions (id, content) VALUES
(gen_random_uuid(), 'Investment Choice'),
(gen_random_uuid(), 'Stock Market Volatility'),
(gen_random_uuid(), 'High-Risk, High-Reward Investments'),
(gen_random_uuid(), 'Market Downturn Reaction'),
(gen_random_uuid(), 'Unexpected Financial Gain'),
(gen_random_uuid(), 'Insurance Purchase'),
(gen_random_uuid(), 'Emergency Fund'),
(gen_random_uuid(), 'Retirement Portfolio'),
(gen_random_uuid(), 'Retirement Plan Adjustments'),
(gen_random_uuid(), 'Previous Financial Decisions'),
(gen_random_uuid(), 'Investment Knowledge'),
(gen_random_uuid(), 'Previous Investment Experience'),
(gen_random_uuid(), 'Financial Goals'),
(gen_random_uuid(), 'Investment Time Horizon'),
(gen_random_uuid(), 'Ethical Investing'),
(gen_random_uuid(), 'Market Trends'),
(gen_random_uuid(), 'Job Security Impact'),
(gen_random_uuid(), 'Future Financial Outlook'),
(gen_random_uuid(), 'Seeking Financial Advice'),
(gen_random_uuid(), 'Influence of Others');

-- Insert answers
INSERT INTO answers (id, content, points, question_id) VALUES
(gen_random_uuid(), 'Invest $1,000 in a savings account with a guaranteed 2% annual return.', 1, (SELECT id FROM questions WHERE content = 'Investment Choice')),
(gen_random_uuid(), 'Invest $1,000 in a mutual fund with an average annual return of 7%, but with the possibility of losing up to 10% in a given year.', 2, (SELECT id FROM questions WHERE content = 'Investment Choice')),

(gen_random_uuid(), 'I prefer to avoid stocks because their value can fluctuate significantly.', 1, (SELECT id FROM questions WHERE content = 'Stock Market Volatility')),
(gen_random_uuid(), 'I am comfortable investing in stocks, even though they can be volatile, because they offer higher returns over the long term.', 2, (SELECT id FROM questions WHERE content = 'Stock Market Volatility')),

(gen_random_uuid(), 'I would never consider investing in a startup company due to the high risk.', 1, (SELECT id FROM questions WHERE content = 'High-Risk, High-Reward Investments')),
(gen_random_uuid(), 'I would consider investing a small portion of my portfolio in a startup for the potential high returns.', 2, (SELECT id FROM questions WHERE content = 'High-Risk, High-Reward Investments')),

(gen_random_uuid(), 'If the stock market dropped 20%, I would sell my investments to avoid further losses.', 1, (SELECT id FROM questions WHERE content = 'Market Downturn Reaction')),
(gen_random_uuid(), 'If the stock market dropped 20%, I would hold my investments and wait for the market to recover.', 2, (SELECT id FROM questions WHERE content = 'Market Downturn Reaction')),
(gen_random_uuid(), 'If the stock market dropped 20%, I would buy more investments, taking advantage of lower prices.', 3, (SELECT id FROM questions WHERE content = 'Market Downturn Reaction')),

(gen_random_uuid(), 'If I received an unexpected bonus of $10,000, I would put it in a high-yield savings account.', 1, (SELECT id FROM questions WHERE content = 'Unexpected Financial Gain')),
(gen_random_uuid(), 'If I received an unexpected bonus of $10,000, I would invest it in the stock market.', 2, (SELECT id FROM questions WHERE content = 'Unexpected Financial Gain')),
(gen_random_uuid(), 'If I received an unexpected bonus of $10,000, I would use it to buy cryptocurrency.', 3, (SELECT id FROM questions WHERE content = 'Unexpected Financial Gain')),

(gen_random_uuid(), 'I always purchase insurance for valuable items to avoid potential losses.', 1, (SELECT id FROM questions WHERE content = 'Insurance Purchase')),
(gen_random_uuid(), 'I sometimes purchase insurance, depending on the item''s value and risk of loss.', 2, (SELECT id FROM questions WHERE content = 'Insurance Purchase')),
(gen_random_uuid(), 'I rarely purchase insurance, preferring to take the risk.', 3, (SELECT id FROM questions WHERE content = 'Insurance Purchase')),

(gen_random_uuid(), 'I prefer to keep a large emergency fund in a savings account for unexpected expenses.', 1, (SELECT id FROM questions WHERE content = 'Emergency Fund')),
(gen_random_uuid(), 'I keep a small emergency fund and invest the rest in higher-return assets.', 2, (SELECT id FROM questions WHERE content = 'Emergency Fund')),
(gen_random_uuid(), 'I don''t maintain an emergency fund because I believe my investments will cover any unexpected expenses.', 3, (SELECT id FROM questions WHERE content = 'Emergency Fund')),

(gen_random_uuid(), 'My retirement portfolio consists mostly of low-risk bonds and savings accounts.', 1, (SELECT id FROM questions WHERE content = 'Retirement Portfolio')),
(gen_random_uuid(), 'My retirement portfolio is a mix of stocks and bonds.', 2, (SELECT id FROM questions WHERE content = 'Retirement Portfolio')),
(gen_random_uuid(), 'My retirement portfolio consists mostly of high-risk, high-reward stocks.', 3, (SELECT id FROM questions WHERE content = 'Retirement Portfolio')),

(gen_random_uuid(), 'I rarely adjust my retirement plan, preferring to keep it stable.', 1, (SELECT id FROM questions WHERE content = 'Retirement Plan Adjustments')),
(gen_random_uuid(), 'I occasionally adjust my retirement plan based on market conditions.', 2, (SELECT id FROM questions WHERE content = 'Retirement Plan Adjustments')),
(gen_random_uuid(), 'I frequently adjust my retirement plan to maximize returns, even if it involves higher risks.', 3, (SELECT id FROM questions WHERE content = 'Retirement Plan Adjustments')),

(gen_random_uuid(), 'I tend to avoid financial decisions that involve any risk.', 1, (SELECT id FROM questions WHERE content = 'Previous Financial Decisions')),
(gen_random_uuid(), 'I am comfortable with some risk in my financial decisions.', 2, (SELECT id FROM questions WHERE content = 'Previous Financial Decisions')),
(gen_random_uuid(), 'I often take significant risks in my financial decisions.', 3, (SELECT id FROM questions WHERE content = 'Previous Financial Decisions')),

(gen_random_uuid(), 'I have limited knowledge about different investment options and strategies.', 1, (SELECT id FROM questions WHERE content = 'Investment Knowledge')),
(gen_random_uuid(), 'I have a basic understanding of various investments and how they work.', 2, (SELECT id FROM questions WHERE content = 'Investment Knowledge')),
(gen_random_uuid(), 'I am well-informed about different investment options and actively follow market trends.', 3, (SELECT id FROM questions WHERE content = 'Investment Knowledge')),

(gen_random_uuid(), 'I have never invested in the stock market or other financial assets.', 1, (SELECT id FROM questions WHERE content = 'Previous Investment Experience')),
(gen_random_uuid(), 'I have invested a small amount in stocks or funds but prefer to keep most of my money in safer options.', 2, (SELECT id FROM questions WHERE content = 'Previous Investment Experience')),
(gen_random_uuid(), 'I have invested in various financial assets and actively manage my investment portfolio.', 3, (SELECT id FROM questions WHERE content = 'Previous Investment Experience')),

(gen_random_uuid(), 'My primary financial goal is to preserve my capital and avoid losses.', 1, (SELECT id FROM questions WHERE content = 'Financial Goals')),
(gen_random_uuid(), 'I aim for a balance between preserving my capital and achieving moderate growth.', 2, (SELECT id FROM questions WHERE content = 'Financial Goals')),
(gen_random_uuid(), 'I prioritize high growth, even if it means risking my capital.', 3, (SELECT id FROM questions WHERE content = 'Financial Goals')),

(gen_random_uuid(), 'I prefer short-term investments to minimize risk.', 1, (SELECT id FROM questions WHERE content = 'Investment Time Horizon')),
(gen_random_uuid(), 'I am open to medium-term investments but prefer not to lock my money away for too long.', 2, (SELECT id FROM questions WHERE content = 'Investment Time Horizon')),
(gen_random_uuid(), 'I am comfortable with long-term investments, even if they come with higher risks.', 3, (SELECT id FROM questions WHERE content = 'Investment Time Horizon')),

(gen_random_uuid(), 'I prefer to invest only in companies that are considered safe and socially responsible, regardless of their returns.', 1, (SELECT id FROM questions WHERE content = 'Ethical Investing')),
(gen_random_uuid(), 'I consider both ethical factors and potential returns when making investment decisions.', 2, (SELECT id FROM questions WHERE content = 'Ethical Investing')),
(gen_random_uuid(), 'I prioritize potential returns over ethical considerations in my investments.', 3, (SELECT id FROM questions WHERE content = 'Ethical Investing')),

(gen_random_uuid(), 'I tend to follow popular market trends and invest based on what others are doing.', 1, (SELECT id FROM questions WHERE content = 'Market Trends')),
(gen_random_uuid(), 'I consider market trends but also conduct my own research before making investment decisions.', 2, (SELECT id FROM questions WHERE content = 'Market Trends')),
(gen_random_uuid(), 'I rarely follow market trends and trust my own analysis when making investments.', 3, (SELECT id FROM questions WHERE content = 'Market Trends')),

(gen_random_uuid(), 'I feel that my job security influences my willingness to take financial risks.', 1, (SELECT id FROM questions WHERE content = 'Job Security Impact')),
(gen_random_uuid(), 'My job security has a moderate impact on my financial risk tolerance.', 2, (SELECT id FROM questions WHERE content = 'Job Security Impact')),
(gen_random_uuid(), 'My willingness to take financial risks is not affected by my job security.', 3, (SELECT id FROM questions WHERE content = 'Job Security Impact')),

(gen_random_uuid(), 'I believe that the economy will remain unstable in the coming years, influencing my cautious approach to investing.', 1, (SELECT id FROM questions WHERE content = 'Future Financial Outlook')),
(gen_random_uuid(), 'I think the economy will experience moderate fluctuations, so I plan to be somewhat cautious but open to opportunities.', 2, (SELECT id FROM questions WHERE content = 'Future Financial Outlook')),
(gen_random_uuid(), 'I am optimistic about the economy''s growth and plan to invest aggressively.', 3, (SELECT id FROM questions WHERE content = 'Future Financial Outlook')),

(gen_random_uuid(), 'I rarely seek financial advice and prefer to make decisions independently.', 1, (SELECT id FROM questions WHERE content = 'Seeking Financial Advice')),
(gen_random_uuid(), 'I occasionally seek advice from financial professionals but trust my instincts more.', 2, (SELECT id FROM questions WHERE content = 'Seeking Financial Advice')),
(gen_random_uuid(), 'I frequently consult with financial advisors or trusted sources before making investment decisions.', 3, (SELECT id FROM questions WHERE content = 'Seeking Financial Advice')),

(gen_random_uuid(), 'The financial decisions of my friends or family significantly influence my own choices.', 1, (SELECT id FROM questions WHERE content = 'Influence of Others')),
(gen_random_uuid(), 'I consider the opinions of others but ultimately make my own decisions.', 2, (SELECT id FROM questions WHERE content = 'Influence of Others')),
(gen_random_uuid(), 'I rarely let the opinions of others influence my financial decisions.', 3, (SELECT id FROM questions WHERE content = 'Influence of Others'));
